# Week 5 - Python as an Engineering Tool

## What this week is about

You already know the programming concepts. This week transfers them to Python and uses Python for a practical engineering job: parsing, validating, grouping, and summarizing captured data.

You should be able to:

- read and modify a small Python program;
- create and import modules;
- use lists and dictionaries intentionally;
- read CSV/text data;
- distinguish successful conversion from valid engineering data;
- report malformed records without losing all remaining data;
- separate source, parsing, validation, analysis, and reporting;
- explain which parts should remain unchanged if the source later becomes a serial port.

## Data pipeline

Keep this model visible:

```text
source -> parse -> validate -> model -> analyze -> report
```

## Examples

- [Sample sensor data](examples/sensor-data.csv)
- [Reference analyzer](examples/sensor_log_analyzer.py)

The sample intentionally contains valid and invalid records. Predict which records should fail before running the analyzer.

## Assignment

**[Sensor Log Analyzer](SensorLogAnalyzer.md)**

The standard library is enough. Do not add a GUI or third-party data-analysis framework unless the assignment is later expanded.