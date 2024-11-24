# Anywheeere
[![Video Label](http://img.youtube.com/vi/GK6W1SRfdPQ/0.jpg)](https://youtu.be/GK6W1SRfdPQ)
[![Video Label](http://img.youtube.com/vi/IXJcxbPJsgg/0.jpg)](https://youtu.be/IXJcxbPJsgg)
[![Video Label](http://img.youtube.com/vi/kq1_X9uVbyc/0.jpg)](https://youtu.be/kq1_X9uVbyc)

## 프로젝트 소개
- 세계지도를 구현한 3D 메타버스 환경dptj  AI 도슨트의 텍스트, 음성 설명을 들으며 다른사람들과 같이 여행을 다니는 서비스 

## 목적
- 시간, 공간, 돈 같은 물리적 제약에서 벗어나 가족이나 친구와 같이 실제처럼 구현된 메타버스 환경을 이용해 유명 관광지를 방문

## 기능
- AI 도슨트의 안내(텍스트, 연예인(성시경) 음성)
- 3D 관광명소
- 파노라마 몰입환경
- 멀티플레이
- 해변가 물총게임 플레이

## RAG
### 데이터 셋
- 정형데이터 : 국가, 도시별, 장소, 랜드마크 등 정보 CSV파일
- 비정형데이터 : 랜드마크별 설명 Text파일
- 랜드마크 이름을 외래키로 묶어서 임베딩
- 유튜브에서 추출 후 쪼갠 성시경 음성 파일 및 STT모델을 사용해 추출한 음성 텍스트 파일

### 프롬프트
- 한국어
- VectorDB retriever반영
- if구문과 for문을 자연어 스타일로 상황별 입력

## 모델 
### gpt-4o-mini
### xtts-v2
### whisper-large-v3
### [Checkpoint](https://drive.google.com/file/d/1HHtytkTBMaO8LEtEK4xNvAgEgjn8vtrs/view?usp=sharing)

## TTS 학습결과
<img width="280" alt="제목 없음" src="https://github.com/user-attachments/assets/5e49061f-5b3a-45a8-9664-0df9f3025f72">
- epoch : 31/1000
- time : 36hours
- avg_loss : 3.8572
