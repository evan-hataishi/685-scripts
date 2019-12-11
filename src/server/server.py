from flask import Flask, jsonify, request
from flask_cors import CORS
from nlp.GetScore import inference, max_score
import os

app = Flask(__name__)
CORS(app)

script = ["Does France really have a poor work ethic or is it just myth",
                  "How can entrepreneurs get business loans when they have limited revenue?",
                  "to see the exact same patterns. t-SNE is not deterministic. Relatedly",
                  "tightness of clusters and distances between clusters are not in the class",
                  "What is the most inappropriate experience you’ve had with a student?",
                  "Why is Geoffrey Hinton suspicious of backpropagation and wants AI to start over",
                  "I’ll also engineer a new feature",
                  "What are some examples of Lee Kuan Yew standing up to bigger powers like the US",
                  "japan or China? ghana or morocco? germany or englang?",
                  "Who is the president of lesotho today",
                  "Is it a common thing to like food when stressed?",
                  "In america the biggest economy or not in 2019?",
                  "I just want the semester to end,lol im so tired i cant even sleep anymore"]


@app.route('/api/test', methods=['GET'])
def get_test():
    score = inference("I love food", "food is great")
    return str(score)


@app.route('/api/script', methods=['GET'])
def get_script():
    return '\n'.join(script)


@app.route('/api/upload', methods=['POST'])
def upload_script():
    updated_script = []
    new_script = request.form['script'].encode('ascii', errors='ignore').strip()
    lines = new_script.decode('ascii', errors='ignore').split('\n')
    for line in lines:
        updated_script.append(lines)
    script = updated_script
    return "Success"


@app.route('/api/score', methods=['GET'])
def get_score():
    if 'phrase' not in request.args.keys():
        return '0'
    phrase = request.args.get("phrase")
    parsed_phrase = ' '.join(phrase.split('+'))
    # score = inference(parsed_phrase, "food is great")
    score = max_score(parsed_phrase, script)
    return str(score)


if __name__ == '__main__':
    port = int(os.getenv('PORT', 8080))
    app.run(host='0.0.0.0', port=port, debug=False)
