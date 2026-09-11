# Assignment - Sensor Log Analyzer

Use a CSV input file with this schema:

```text
timestamp,channel,value,status
```

## Required behavior

Your Python program must:

- parse all records without assuming they are valid;
- accept `status` as decimal or `0x` hexadecimal;
- reject status outside `0..255`;
- reject or report malformed numeric/channel data with a useful line-specific message;
- continue processing valid records after a malformed record;
- calculate minimum, maximum, average, and record count per channel;
- count faulted records per channel using bit 7 of the status byte;
- keep parsing/validation separate from reporting;
- use at least two Python source files;
- include your own small edge-case test-data file;
- preserve meaningful stages with Git commits.

## Short architecture explanation

In **150-250 words**, explain which parts of your program should remain unchanged if the data source changes from a file to a serial port and which part should be replaced.

## Testing target

Your test data should include at least:

- two channels;
- minimum/maximum variation;
- a faulted status;
- hexadecimal and decimal status forms;
- a malformed numeric value;
- an empty/invalid field;
- a status value outside one byte.