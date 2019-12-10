from flask import Flask, jsonify, request
from flask_cors import CORS
import os

app = Flask(__name__)
CORS(app)


@app.route('/api/score', methods=['GET'])
def get_score():
    return '100'


port = int(os.getenv('PORT', 8080))
app.run(host='0.0.0.0', port=port, debug=False)
