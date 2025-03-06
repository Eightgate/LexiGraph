
import re
import string
import sys
import json
from collections import Counter

import nltk
from nltk.util import ngrams
from nltk.tokenize import word_tokenize
from nltk.corpus import stopwords

import matplotlib.pyplot as plt

def create_bigram_dictionary_as_json(cleaned_text: str, n: int = 2) -> str:
    tokens = word_tokenize(cleaned_text)
    bigrams = list(ngrams(tokens, n))
    bigram_counts = Counter(bigrams)

    # Convert tuple keys to space-separated strings for JSON
    bigram_counts_str = {" ".join(key): count for key, count in bigram_counts.items()}

    return json.dumps(bigram_counts_str)


def parse_to_dict(input_data) -> dict:
    if isinstance(input_data, str):
        return json.loads(input_data)
    elif isinstance(input_data, dict):
        return input_data
    else:
        raise TypeError("Input must be a JSON string or a dictionary.")
