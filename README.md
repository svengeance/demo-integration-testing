# demo-integration-testing
A simple demonstration of how one might integration test an ASP.NET Core API.

The primary objective of this app is to setup a few dependencies in a quick manner to demonstrate integration testing. The app doesn't make an awful lot of sense, but it _is_ real and functional, which is, I feel, a step up from other examples.

# Setup
In short, the app expects the following configuration (via env vars, string args, user secrets, or appsettings json):

- ConnectionStrings.WeatherDb // sqlite connection string
- WeatherApi.ConnectionString // api key for weather API

## Weather API
Register an account on [WeatherAPI](https://weatherapi.com)(it's free!) and get an API key.

## Sqlite DB
A sqlite database will be created via the connection string. In-memory mode is not supported in the main application, albeit it's used in tests. Consecutive runs will retain data until the database is destroyed.

# Contributors
Contributions and issues are welcome to explain concepts, architecture, or methodologies. Please try to avoid questioning the sensibility/"business value" of the application itself.
