# CS8035 error reading ruleset

Overall this looks a bad interaction between my naming scheme and the MS XML writer.

So far, what I now: as soon as I attempt adding a list of dependencies to the root element, garbage appears in the XML file, later causing the error.

```
<General:Unit xmlns:General="General"><General:namespace General:t="String"></General:namespace><General:deps General:t="List"><General:String>USING...</General:String></General:deps><General:expanded General:t="Boolean">False</General:expanded></General:Unit>
```
