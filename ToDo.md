# To Do

### Logging

- [ ] Integrate structured logging using Serilog with (file and splunk) sinks
- [ ] Establish the correct log levels to log and create standards
- [ ] Do we need Liblog in service libraries?

### IoC
	
- [ ] Decide between DryIoC and SimlpleInjector (both support verification which can be wrapped in UTs)
- [ ] Create standard registrations for f/w level dependencies (nHibernate,IHttpContext)
- [ ] Registration tests coverage?

### OR/M

- [ ] Fluent or Mapping by Code?
- [ ] Integrate Serilog to nH for removing dependency on log4net.
- [ ] Mapping tests coverage

### AMPS
- [ ] Create f/w to publish to AMPS
- [ ] Can we get inspired from Serilog in terms of core message and then multiple sinks for AMPS vs File vs Email?
- [ ] Add diagnostics?


### JOLT

- [ ] Create a standard JoltService to deal with all interactions with Jolt.
    - CreateCase (with both start and complete or autocomplete)
    - GetCaseById
    - GetCaseByUserAndQuery
    - ExecuteTask
- [ ] ***Can this be a micro service listening on a Jolt Amps topic / http completely decoupled***?
- [ ] Can this also listen to MQ messages from Jolt to complete the workfow?
- [ ] Needs its own diagnostics in terms of logging and database?