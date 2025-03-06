import os
import json
import pytest
import matplotlib.pyplot as plt

# Import functions from your Python modules.
# Adjust the module names as necessary. Here we assume the files are:
# PYTHON_Text_Prep.py, PYTHON_Ngram_Builder.py, PYTHON_Generate_Histogram.py.
from PYTHON_Text_Prep import clean_text
from PYTHON_Ngram_Builder import create_bigram_dictionary_as_json
from PYTHON_Generate_Histogram import generate_histogram


class TestTextPrep:
    def test_clean_text_returns_cleaned_text(self):
        """
        Test that clean_text returns the expected cleaned version.
        """
        input_text = 'Hello, World!'
        result = clean_text(input_text)
        expected = 'hello world'
        assert result == expected

    def test_clean_text_removes_newlines_and_extra_whitespace(self):
        """
        Test that clean_text removes newline characters and extra whitespace.
        """
        # Example input containing newlines, carriage returns, and extra spaces.
        input_text = "Line one.\n        Line two.\r\n Line three.s" # dirty example
        expected = "line one line two line threes"                   # hoped for clean result 
    
        from PYTHON_Text_Prep import clean_text  # import setup 
    
        result = clean_text(input_text)
        assert result == expected



class TestNgramBuilder:
    def test_create_bigram_dictionary_as_json_returns_valid_json(self):
        """
        Test that create_bigram_dictionary_as_json returns a JSON string
        that can be parsed into a dictionary with expected keys and values.
        """
        input_text = "hello world"
        result_json = create_bigram_dictionary_as_json(input_text)
        # Parse the JSON string.
        result_dict = json.loads(result_json)
        # For our dummy implementation, we assume the function returns {"hello world": 1}
        assert isinstance(result_dict, dict)
        assert "hello world" in result_dict
        assert result_dict["hello world"] == 1


class TestHistogramGenerator:
    @pytest.fixture(autouse=True)
    def setup_and_teardown(self, tmp_path, monkeypatch):
        """
        Fixture that changes the working directory to a temporary path,
        and overrides plt.show() so that it doesn't block the test.
        After the test, it cleans up the generated histogram file.
        """
        # Save the original working directory.
        original_dir = os.getcwd()
        os.chdir(tmp_path)
        # Override plt.show to do nothing.
        monkeypatch.setattr(plt, "show", lambda: None)
        yield
        # Teardown: revert to the original directory.
        os.chdir(original_dir)
        # Remove the generated file if it exists.
        histogram_file = os.path.join(tmp_path, "bigram_histogram.png")
        if os.path.exists(histogram_file):
            os.remove(histogram_file)

    def test_generate_histogram_creates_file(self):
        """
        Test that calling generate_histogram with a valid JSON string creates
        the expected output file 'bigram_histogram.png'.
        """
        # Create a sample JSON string representing n-gram counts.
        test_json = json.dumps({"hello world": 1})
        generate_histogram(test_json)
        # Check that the histogram file exists.
        assert os.path.exists("bigram_histogram.png")
