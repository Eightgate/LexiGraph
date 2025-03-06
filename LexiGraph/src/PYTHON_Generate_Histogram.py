import re
import string
import sys
import json
from collections import Counter

import matplotlib.pyplot as plt

def generate_histogram(input_data: str) -> None:
    """
    Generates a histogram from a JSON object or dictionary containing bigram counts.
    
    Parameters:
        input_data: Either a JSON string or a dictionary with bigram counts.
    
    Returns:
        None
    """
#    bigram_dict = parse_to_dict(input_data)

    bigram_dict = json.loads(input_data)

    # Prepare labels and counts.
    labels = [' '.join(key) if isinstance(key, (list, tuple)) else key 
              for key in bigram_dict.keys()]
    counts = list(bigram_dict.values())
    
    # Generate the histogram.
    plt.figure(figsize=(10, 6))
    plt.bar(labels, counts)
    plt.xlabel("Bigrams")
    plt.ylabel("Frequency")
    plt.title("Bigram Frequency Histogram")
    plt.xticks(rotation=45, ha="right")
    plt.tight_layout()
    plt.savefig("bigram_histogram.png")
    plt.show()

if __name__ == "__main__":
    if len(sys.argv) > 1:
        input_text = " ".join(sys.argv[1:])
        cleaned = clean_text(input_text)
        counted = create_bigram_dictionary(cleaned)
        print(counted)
        generate_histogram(counted)
    else:
        print("Usage: python text_cleaner.py \"Your text here\"")
