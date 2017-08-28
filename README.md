# Concordance_test
Implementation of the Concordance test case

Following assumptions were made:
1. Shortenings should be used as words.
2. All words can be cast to lowercase
3. If a given word appears in a sentence more than once, every appearance should be marked differently (for example -> ff. word {3:1,1,2} appears twice in the first sentence)
	By the way this behavior can be switched off by s simple code fix

Notes:
Please note that one unit test should fail everytime. Earlier I have mentioned the assumption (3) and I have created unit tests for both scenarios. 

Regex for splitting a string into sentences was taken from here: https://stackoverflow.com/questions/25735644/python-regex-for-splitting-text-into-sentences-sentence-tokenizing
A very minor modification was made to add an exclamation mark as a sentence end.
	
