from flask import Flask, request, jsonify
from transformers import AutoTokenizer, AutoModelForCausalLM, pipeline
import torch

app = Flask(__name__)

model_name = "sberbank-ai/rugpt3small_based_on_gpt2"
tokenizer = AutoTokenizer.from_pretrained(model_name)
model = AutoModelForCausalLM.from_pretrained(model_name)

generator = pipeline("text-generation", model=model, tokenizer=tokenizer, device=0 if torch.cuda.is_available() else -1, max_new_tokens=80)

@app.route("/recommend", methods=["POST"])
def recommend():
    data = request.get_json()
    text = data.get("text", "")
    emotions = data.get("emotions", {})

    if not text or not emotions:
        return jsonify({"error": "Missing text or emotions"}), 400

    emo_str = ", ".join(f"{k}: {v:.2f}" for k, v in emotions.items())
    prompt = f"Текст пользователя: {text}\nЭмоции: {emo_str}\n\nРекомендация:"

    output = generator(prompt, do_sample=True, top_k=50, temperature=0.9)[0]["generated_text"]

    recommendation = output.split("Рекомендация:")[-1].strip().split("\n")[0]

    return jsonify({"recommendation": recommendation})

@app.route("/health")
def health():
    return "ok"

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5001)