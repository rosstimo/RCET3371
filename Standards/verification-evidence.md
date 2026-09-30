# Verification Evidence Standard

[Standards index](README.md)

## Verification chain

For important requirements preserve:

1. requirement;
2. test/procedure;
3. expected result;
4. actual result/evidence;
5. conclusion.

## Acceptable evidence

Depending on the requirement:

- automated test output;
- exact input/output transcript;
- captured protocol bytes;
- measured timing;
- log excerpt;
- debugger/register evidence;
- clean-clone build;
- screenshot for genuinely visual behavior;
- Git commit/tag identifying tested version.

## Weak evidence

These alone are usually insufficient:

- "works";
- one unexplained screenshot;
- source code constant without measurement;
- successful build used as proof of runtime behavior;
- live demo with no reproducible procedure.

## Boundary and failure cases

Verify:

- minimum/maximum;
- threshold and just below/above;
- empty/one/full;
- malformed input;
- timeout;
- missing file/config;
- disconnect/reconnect;
- invalid transition;
- unavailable hardware.

## Simulation versus hardware

Clearly label:

- deterministic simulated/fake verification;
- physical hardware verification.

Do not claim physical verification from a simulation.

## Reproducibility

Another student/instructor should be able to repeat the procedure from the repository and obtain equivalent results.
