import os 
import torch
import torchaudio
from TTS.tts.configs.xtts_config import XttsConfig
from TTS.tts.models.xtts import Xtts

class AiSpeech:
    def __init__(self):
        self.text = None
        
        tokenizer_path = 'XTTS_v2.0_original_model_files/vocab.json'
        model_path = 'tts_model/tts_model.pth'
        config_path = 'tts_model/config.json'
        config = XttsConfig()
        config.load_json(config_path)
        
        self.model = Xtts.init_from_config(config)
        self.model.load_checkpoint(config, checkpoint_path=model_path, vocab_path=tokenizer_path, use_deepspeed=False)
        self.model.cuda()
        
        self.gpt_cond_latent, self.speaker_embedding = self.model.get_conditioning_latents(audio_path=['voice_data/wavs/output_6_195.wav'])
    
    def inference(self, text):
        self.text = text
        out = self.model.inference(
            self.text,
            'ko',
            self.gpt_cond_latent,
            self.speaker_embedding,
            temperature=0.3        
        )    
        
        torchaudio.save('output.wav', torch.tensor(out["wav"]).unsqueeze(0), 24000)
        return True