from flask import Flask, jsonify, request
from flask_cors import CORS
from nlp.GetScore import inference
import os

app = Flask(__name__)
CORS(app)


@app.route('/api/test', methods=['GET'])
def get_test():
    score = inference("I love food", "food is great")
    return str(score)


@app.route('/api/score', methods=['GET'])
def get_score():
    if 'phrase' not in request.args.keys():
        return '0'
    phrase = request.args.get("phrase")
    parsed_phrase = ' '.join(phrase.split('_'))
    score = inference(parsed_phrase, "food is great")
    return score


if __name__ == '__main__':
    port = int(os.getenv('PORT', 8080))
    app.run(host='0.0.0.0', port=port, debug=False)
