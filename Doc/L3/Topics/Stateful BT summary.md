# Stateful BT summary

- Do not commit to "stateful by default", either in fact or in principle.
- Stateful BTs hold *strong appeal* with software engineers and designers. Software engineers tend to address failure as always undesirable, always unexpected. They believe everything can be fixed, and made reliable. In mixed systems (human/machine, real/virtual) this is not going to pan out.
- Start from stateless BTs.
- We need memory nodes to address situations where (1) doing the task IS the desired outcome and (2) the outcome is delayed (3) validation is expensive, so we only do it "some of the time" or (4) we cannot *afford* the engineering overhead *right now*.
- In all cases, where significant value is at stake, harden memory node validation (needs investigation).
- For debugging purposes, screen memory nodes and flag 'weak nodes' which are expected to generate bugs; this will help with diagnostics if we need to ship incomplete models.
- Memory nodes provide finer control than ordered sequences.
- Ordered sequences are useful when we cannot verify in-between steps, however we still commit to verify the overall outcome
- "leave the nodes ticked" often the better strategy.
- Would focus on avoiding stateful delegates
- Would use labels to flag unverified tasks (so that we can draft quickly and perhaps ship incomplete models, but with a clear view on what to look for when debuggin models)
- Notification hooks and policies for "delayed verification"
- Use a black box.
- Stateful control is not for keeping tabs.
