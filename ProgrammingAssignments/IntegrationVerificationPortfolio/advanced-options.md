# Advanced Extension Options

Choose one option unless instructed otherwise.

## A — Automated regression tests

Complete when:

- at least five meaningful previously manual cases are automated;
- at least one is a failure/boundary regression;
- one documented command runs the suite;
- tests fail if the repaired behavior is deliberately re-broken;
- evidence explains why each case matters.

## B — Async/cancelable I/O

Complete when:

- a formerly blocking wait is made asynchronous/cancelable;
- cancellation/lifecycle is explicit;
- exceptions are observed/reported;
- domain/parser tests remain deterministic;
- evidence demonstrates responsiveness or overlapping wait behavior without unsupported performance claims.

## C — HTTP/JSON status API

Complete when:

- a read-only endpoint exposes selected model state;
- JSON schema/example is documented;
- invalid/unready state has defined response;
- startup/shutdown is documented;
- core model does not depend on HTTP.

## D — Packaging/CI

Complete when:

- a clean runner restores/builds/tests;
- generated artifacts are separated from source;
- a deliberately introduced known failure makes the pipeline fail;
- local commands match CI behavior;
- README documents reproduction.

## E — Compiler/disassembly investigation

Complete when:

- one bounded function/algorithm is selected;
- compiler/tool version and optimization setting are recorded;
- source behavior is verified first;
- generated assembly/disassembly is compared for two meaningful variants;
- timing/size claims are backed by measurement or tool output;
- conclusion includes limitations.
