from flask import Flask, request, jsonify
from transformers import pipeline
from collections import defaultdict

app = Flask(__name__)

# Загружаем модель
classifier = pipeline("text-classification", model="r1char9/rubert-tiny2-ru-go-emotions", top_k=None)

# Сопоставление GoEmotions с пользовательскими категориями
emotion_mapping = {
    "Joy": ["joy", "approval", "gratitude", "admiration", "excitement", "amusement", "relief", "pride", "love", "desire", "optimism"],
    "Sadness": ["sadness", "grief", "remorse", "disappointment", "embarrassment"],
    "Anger": ["anger", "annoyance", "disapproval"],
    "Calmness": ["neutral", "realization", "caring"],
    "Anxiety": ["fear", "nervousness"],
    "Confusion": ["confusion", "curiosity", "surprise"]
}

# Инвертируем маппинг
label_to_emotion = {}
for emotion, labels in emotion_mapping.items():
    for label in labels:
        label_to_emotion[label] = emotion

@app.route("/analyze", methods=["POST"])
def analyze():
    data = request.get_json()
    text = data.get("text", "")
    
    if not text:
        return jsonify({"error": "No text provided"}), 400
    
    result = classifier(text)[0]  # берем только первую запись (batch из 1 текста)
    
    emotion_scores = defaultdict(float)
    total = 0.0

    # Группируем по своим эмоциям
    for item in result:
        label = item["label"]
        score = item["score"]
        mapped = label_to_emotion.get(label)
        if mapped:
            emotion_scores[mapped] += score
            total += score

    # Нормализуем до суммы 1 (если total > 0)
    if total > 0:
        for key in emotion_scores:
            emotion_scores[key] = round(emotion_scores[key] / total, 4)

    return jsonify(dict(emotion_scores))

@app.route("/health")
def health():
    return "ok"

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)