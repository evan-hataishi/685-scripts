import scipy
import numpy as np
import warnings
warnings.filterwarnings('ignore')
import numpy as np
import io
import sys

def compare(str_input1, str_input2):
	#print("String1: %s"%str_input1)
	#print("String2: %s"%str_input2)

	pp1 = str_input1.strip().split()
	pp2 = str_input2.strip().split()
	pp3 = set(pp1+pp2)
	print(len(pp3))
	#return len(pp3)

if __name__=="__main__":
	if len(sys.argv)!=2+1:
		print("This function takes 2 parameters, %s provided"%(len(sys.argv)-1))
	else:
		compare(str(sys.argv[0+1]),str(sys.argv[1+1]))