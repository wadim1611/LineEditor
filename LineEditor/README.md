# line-oriented text editor
**Goal:**
Write a line-oriented text editor that reads a text file and allows basic editing commands

**Usage:**
lineeditor c:\temp\myfile.txt
(displays a >> prompt)

**Commands:**

- list - list each line in n:xxx format, e.g.:
> 1: first line
> 2: second line
> 3: last line
- d | del <line_index> - delete line at n
- i | ins <line_index> <line> - insert a line at n
- s | save - saves to disk
- u | undo - cancel last operation
- r | redo - redo cancelled operation to disk
- quit - quits the editor and returns to the command line

