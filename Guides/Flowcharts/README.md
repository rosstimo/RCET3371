# RCET Flowchart Guide

These are the current RCET student-facing resources for program-flowchart design and documentation.

- [RCET Flowcharts for Program Design and Documentation](RCET-Flowchart-Guide.md) - the complete guide covering design workflow, notation, decisions and loops, level of detail, hierarchical charts, interrupts, lab-book documentation, Mermaid, draw.io, and hand-drawn flowcharts.
- [RCET Flowchart Quick Reference](RCET-Flowchart-Quick-Reference.md) - a compact companion for students who already know what they need to draw and want fast notation, Mermaid, draw.io, and checking reminders.

The guide is the authority when the quick reference omits detail. Assignment-specific requirements still take precedence where a lab or course asks for something more specific.

## Mermaid documentation convention

Keep Mermaid diagrams source-first whenever practical:

1. include an **Open this diagram in Mermaid Live Editor** link;
2. keep the Mermaid source in a fenced `mermaid` block directly in the Markdown;
3. rely on GitHub's Mermaid rendering when viewing the Markdown on GitHub;
4. create PNG, SVG, PDF, or other rendered assets only when a particular slide, quiz, handout, or exported document needs an image.

This keeps diagrams editable, reviewable, and version-controlled without maintaining duplicate source and image files.

## Scope

The guidance is intended to be reusable across RCET programming and embedded-systems courses. Some worked examples come from RCET3375 PIC assembly labs because they provide useful concrete examples of setup, decisions, loops, subroutines, `GOTO`, and interrupts.

**Status:** current Fall 2026 guidance.
