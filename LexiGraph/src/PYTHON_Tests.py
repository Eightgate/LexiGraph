import os
import json
import tempfile
import pytest
from PYTHON_Generate_Histogram import clean_text, create_bigram_dictionary, parse_to_dict, generate_histogram
import matplotlib.pyplot as plt

def test_clean_text_basic():
    # Test that clean_text lowercases, removes punctuation, and normalizes whitespace.
    input_text = "Hello,   World!  This is a test."
    expected = "hello world this is a test"
    result = clean_text(input_text)
    assert result == expected

def test_clean_text_remove_stopwords():
    # Test that stopwords are removed when requested.
    input_text = "This is a simple test of the system."
    # Assuming the stopwords list includes common words like "this", "is", "a", "of", "the".
    expected = "simple test system"
    result = clean_text(input_text, remove_stopwords=True)
    assert result == expected

def test_clean_text_remove_numbers():
    # Test that numbers are removed when requested.
    input_text = "Test 123, this is 456."
    # After processing: lowercase -> "test 123 this is 456"
    # Remove numbers -> "test  this is "
    # Normalize whitespace -> "test this is"
    expected = "test this is"
    result = clean_text(input_text, remove_numbers=True)
    assert result == expected

def test_create_bigram_dictionary():
    # Given cleaned text "hello world hello"
    # Tokens: ["hello", "world", "hello"]
    # Bigrams: [("hello", "world"), ("world", "hello")]
    cleaned_text = "hello world hello"
    expected = {("hello", "world"): 1, ("world", "hello"): 1}
    result = create_bigram_dictionary(cleaned_text)
    assert result == expected

def test_parse_to_dict_with_dict():
    # When passing a dictionary, parse_to_dict should return it unchanged.
    input_dict = {("hello", "world"): 2}
    result = parse_to_dict(input_dict)
    assert result == input_dict

def test_parse_to_dict_with_json():
    # When passing a JSON string, it should return the equivalent dictionary.
    # Note: JSON keys must be strings. So we'll use a dict with string keys.
    input_dict = {"hello world": 2}
    json_str = json.dumps(input_dict)
    result = parse_to_dict(json_str)
    assert result == input_dict

def test_parse_to_dict_invalid():
    # Test that a non-string, non-dictionary input raises TypeError.
    with pytest.raises(TypeError):
        parse_to_dict(123)

def test_generate_histogram(tmp_path, monkeypatch):
    """
    Test that generate_histogram creates an output file.
    We use monkeypatch to override plt.show() so it doesn't block,
    and we change the working directory to a temporary directory.
    """
    # Create a simple bigram dictionary.
    bigram_dict = {("hello", "world"): 3, ("world", "hello"): 2}
    # Override plt.show() to prevent the plot window from blocking the test.
    monkeypatch.setattr(plt, "show", lambda: None)
    
    # Change the current working directory to the temporary path.
    old_cwd = os.getcwd()
    os.chdir(tmp_path)
    
    try:
        generate_histogram(bigram_dict)
        # Check that the output file exists.
        output_file = tmp_path / "bigram_histogram.png"
        assert output_file.exists()
    finally:
        # Restore the original working directory.
        os.chdir(old_cwd)


