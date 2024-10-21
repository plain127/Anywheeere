import os
from trainer import Trainer, TrainerArgs
from TTS.tts.configs.glow_tts_config import GlowTTSConfig
from TTS.config.shared_configs import BaseDatasetConfig
from TTS.tts.datasets import load_tts_samples
from TTS.tts.layers.xtts.trainer.gpt_trainer import GPTArgs, GPTTrainer, GPTTrainerConfig, XttsAudioConfig
from TTS.utils.manage import ModelManager

RUN_NAME = 'GPT_XTTS_V2.0_DOCENT_FT'
PROJECT_NAME = 'XTTS_trainer'
DASHBOARD_LOGGER = 'tensorboard'
LOGGER_URI = None

OUT_PATH = os.path.dirname(os.path.abspath(__file__))

OPTIMIZER_WD_ONLY_ON_WEIGHTS = True
START_WITH_EVAL = True
BATCH_SIZE = 3
GRAD_ACUMM_STEPS = 84

dataset_config = BaseDatasetConfig(
    formatter='ljspeech',
    dataset_name='voice_data',
    meta_file_train='metadata.txt',
    path='./voice_data/',
    language='ko'
)

DATASETS_CONFIG_LIST = [dataset_config]

CHECKPOINTS_OUT_PATH =  os.path.join(OUT_PATH, "XTTS_v2.0_original_model_files/")
os.makedirs(CHECKPOINTS_OUT_PATH, exist_ok=True)

DVAE_CHECKPOINT_LINK = 'https://coqui.gateway.scarf.sh/hf-coqui/XTTS-v2/main/dvae.pth'
MEL_NORM_LINK = 'https://coqui.gateway.scarf.sh/hf-coqui/XTTS-v2/main/mel_stats.pth'

DVAE_CHECKPOINT = os.path.join(CHECKPOINTS_OUT_PATH, os.path.basename(DVAE_CHECKPOINT_LINK))
MEL_NORM_FILE = os.path.join(CHECKPOINTS_OUT_PATH, 'mel_stats.pth')

if not os.path.isfile(DVAE_CHECKPOINT) or not os.path.isfile(MEL_NORM_FILE):
    print(" > Downloading DVAE files!")
    ModelManager._download_model_files([MEL_NORM_LINK, DVAE_CHECKPOINT_LINK], CHECKPOINTS_OUT_PATH, progress_bar=True)

TOKENIZER_FILE_LINK = 'https://coqui.gateway.scarf.sh/hf-coqui/XTTS-v2/main/vocab.json'
XTTS_CHECKPOINT_LINK = 'https://coqui.gateway.scarf.sh/hf-coqui/XTTS-v2/main/model.pth'

TOKENIZER_FILE = os.path.join(CHECKPOINTS_OUT_PATH, os.path.basename(TOKENIZER_FILE_LINK))  
XTTS_CHECKPOINT = os.path.join(CHECKPOINTS_OUT_PATH, os.path.basename(XTTS_CHECKPOINT_LINK))

if not os.path.isfile(TOKENIZER_FILE) or not os.path.isfile(XTTS_CHECKPOINT):
    print(" > Downloading XTTS v2.0 files!")
    ModelManager._download_model_files(
        [TOKENIZER_FILE_LINK, XTTS_CHECKPOINT_LINK], CHECKPOINTS_OUT_PATH, progress_bar=True
    )
    
SPEAKER_REFERENCE = [
    "./voice_data/wavs/output_4_26.wav" 
]
LANGUAGE = dataset_config.language

def main():
    model_args = GPTArgs(
        max_conditioning_length=132300,
        min_conditioning_length=66150,
        debug_loading_failures=False,
        max_wav_length=255995,
        max_text_length=200,
        mel_norm_file=MEL_NORM_FILE,
        dvae_checkpoint=DVAE_CHECKPOINT,
        xtts_checkpoint=XTTS_CHECKPOINT,
        tokenizer_file=TOKENIZER_FILE,
        gpt_num_audio_tokens=1026,
        gpt_start_audio_token=1024,
        gpt_stop_audio_token=1025,
        gpt_use_perceiver_resampler=True,
        gpt_use_masking_gt_prompt_approach=True
        )


    
    audio_config = XttsAudioConfig(sample_rate=22050, dvae_sample_rate=22050, output_sample_rate=24000)
    
    config = GPTTrainerConfig(
        output_path=OUT_PATH,
        model_args=model_args,
        run_name=RUN_NAME,
        project_name=PROJECT_NAME,
        run_description="""
            GPT XTTS training
            """,
        dashboard_logger=DASHBOARD_LOGGER,
        logger_uri=LOGGER_URI,
        audio=audio_config,
        batch_size=BATCH_SIZE,
        batch_group_size=48,
        eval_batch_size=BATCH_SIZE,
        num_loader_workers=8,
        eval_split_max_size=256,
        print_step=50,
        plot_step=100,
        log_model_step=1000,
        save_step=10000,
        save_n_checkpoints=1,
        save_checkpoints=True,
        print_eval=False,
        optimizer="AdamW",
        optimizer_wd_only_on_weights=OPTIMIZER_WD_ONLY_ON_WEIGHTS,
        optimizer_params={"betas": [0.9, 0.96], "eps": 1e-8, "weight_decay": 1e-2},
        lr=5e-06, 
        lr_scheduler="StepLR",
        lr_scheduler_params={"step_size": 50, "gamma": 0.5, "last_epoch": -1},
        test_sentences=[
            {
                'text':'자, 드디어 뉴욕에 도착했습니다! 이곳은 수많은 꿈이 이루어지는 곳이자, 세계에서 가장 상징적인 도시 중 하나예요. 뉴욕에는 자유의 여신상, 엠파이어 스테이트 빌딩, 타임스퀘어와 같은 랜드마크들이 즐비해 있죠. 어디를 먼저 둘러보고 싶으신가요? 여러분의 선택에 따라 뉴욕의 매력을 하나하나 탐험해보도록 할게요!',
                'speaker_wav':SPEAKER_REFERENCE,
                'language':LANGUAGE,
            },
             {
                'text':'여러분, 자유의 여신상에 오신 것을 환영합니다! 이 상징적인 동상은 미국의 자유와 민주주의를 상징하며, 1886년 프랑스로부터 미국에 선물로 주어졌죠',
                'speaker_wav':SPEAKER_REFERENCE,
                'language':LANGUAGE,
            }
        ]
    )
    
    model = GPTTrainer.init_from_config(config)
    
    train_samples, eval_samples = load_tts_samples(
        DATASETS_CONFIG_LIST,
        eval_split=True,
        eval_split_max_size=config.eval_split_max_size,
        eval_split_size=config.eval_split_size,
    )
    
    trainer = Trainer(
        TrainerArgs(
            restore_path=None,
            skip_train_epoch=False,
            start_with_eval=START_WITH_EVAL,
            grad_accum_steps=GRAD_ACUMM_STEPS,
        ),
        config,
        output_path=OUT_PATH,
        model=model,
        train_samples=train_samples,
        eval_samples=eval_samples,
    )
    trainer.fit()
    
if __name__ == '__main__':
    main()