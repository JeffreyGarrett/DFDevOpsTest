# DFDevOpsTest
The `Durable` directory contains the starter, orchestrator, and activity functions. The `Durable.Mstest` directory contains a unit test project. Currently, that just contains 3 unit tests that all assert true.

# Settings
The only setting needed is `AzureWebJobsStorage` which should be set to the connection string of the Azure storage.

# Running the Function
Once deployed, send a POST to `/api/DevOpsStarter` with JSON

```
{
	"delay": "5",
	"speed": "3"
}
```

Where `delay` is how many _seconds_ to wait before triggering the activity and `speed` is how many _seconds_ that the activity will take to run.
