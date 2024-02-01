# An XML error

I just seem to have trouble parsing xml right now.
The following causes an error in the reader, at line 120

```xml
<L3Script><value t="Unit"><ns t="String"></ns>
<nodes t="List">
<Function><type t="String">void</type><name t="String">Greet</name><parameters t="List"><Parameter><type t="String">string</type><name t="String">arg</name></Parameter></parameters><expression t="Call"><function t="String">Log</function><opt t="Boolean">False</opt><args t="List"><Var><value t="String">arg</value><opt t="Boolean">False</opt></Var></args><expanded t="Boolean">False</expanded></expression><expanded t="Boolean">False</expanded></Function><Call><function t="String">Greet</function><opt t="Boolean">False</opt><args t="List"><LString><value t="String">Hello World</value></LString></args><expanded t="Boolean">False</expanded></Call></nodes><expanded t="Boolean">False</expanded></value></L3Script>
```

Yep... XML reader error
