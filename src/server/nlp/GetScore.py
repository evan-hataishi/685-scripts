import scipy as sc
import numpy as np
import warnings
warnings.filterwarnings("ignore")

from gensim.models import Word2Vec
import sys
from nltk.tokenize import word_tokenize as wt
from keras.models import load_model
from tensorflow import keras

import tensorflow as tf

session = tf.Session()
keras.backend.set_session(session)

print("Loading Models")
siamese_model = load_model('nlp/my_model_siamese_Lstm_final.h5')
model1 = Word2Vec.load("nlp/word2vec_256.model")
print("Done loading models")


def predict_score(x1, x2):
    with session.as_default():
        with session.graph.as_default():
            sim_prob = siamese_model.predict([x1,x2])
            return sim_prob[0][0]


def inference(x1, x2):
    #tokenize and pad
    x1=wt(x1.lower().strip())
    x2=wt(x2.lower().strip())

    if len(x1)>=16:
        x1=x1[:16]
    else:
        while(len(x1)<16):
            x1.append("pad")

    if len(x2)>=16:
        x2=x2[:16]
    else:
        while(len(x2)<16):
            x2.append("pad")

    #print("--------words----------")
    #print(x1)
    #print(x2)
    q1=[]
    q2=[]
    for word in x1:
        try:
            q1.append(model1.wv.word_vec(word))
        except Exception as e:
            q1.append(model1.wv.word_vec("pad"))
            continue
    for word2 in x2:
        try:
            q2.append(model1.wv.word_vec(word2))
        except Exception as e2:
            q2.append(model1.wv.word_vec("pad"))
            continue

    x1 = np.asarray(q1,dtype='float32').reshape((1,16,256))
    x2 = np.asarray(q2,dtype='float32').reshape((1,16,256))
    try:
        return predict_score(x1, x2)
    except:
        return ''


# if __name__=="__main__":
#     x1_input = sys.argv[1]
#     x2_input = sys.argv[2]
#     #x1_input  = "I love food"
#     #x2_input = "food is great"
#     print(inference(x1_input,x2_input)[0][0])
