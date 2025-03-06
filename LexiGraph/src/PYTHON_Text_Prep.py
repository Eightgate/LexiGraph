import re
import string
import sys
import json

import nltk
from nltk.util import ngrams
from nltk.tokenize import word_tokenize
from nltk.corpus import stopwords

import matplotlib.pyplot as plt

def clean_text(input_text, remove_stopwords=False, remove_numbers=False):
    """
    Cleans the input_text by:
      - Converting to lowercase.
      - Removing punctuation.
      - Collapsing extra whitespace.
    Optionally, it removes stopwords and numbers.
    
    Parameters:
        input_text (str): The text to be cleaned.
        remove_stopwords (bool): If True, removes English stopwords (default: False).
        remove_numbers (bool): If True, removes numeric digits (default: False).
    
    Returns:
        str: The cleaned text.
    """
    # Convert text to lowercase.
    text = input_text.lower()
    
    # Remove punctuation using str.translate.
    text = text.translate(str.maketrans('', '', string.punctuation))

    # Remove newlines and returns
    chars_to_remove = string.whitespace.replace(" ", "")
    text = text.translate(str.maketrans('', '', chars_to_remove))

    # Remove extra white space
    text = " ".join(text.split())


    # remove stopwords.
    if remove_stopwords:
        try:
            stop_words = set(stopwords.words('english'))
        except LookupError:
            nltk.download('stopwords')
            stop_words = set(stopwords.words('english'))
        words = text.split()
        words = [word for word in words if word not in stop_words]
        text = ' '.join(words)
    
    # TODO lemmatize

    return text
