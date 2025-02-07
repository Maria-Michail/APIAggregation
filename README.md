# API Aggregation Service
This is a simple API Aggregation Service that aggregates data from multiple external APIs (weather, news, and movies) into a single response. It provides an easy-to-use endpoint to retrieve combined data based on user-provided parameters such as location, date, and sorting options.

**Features**
- Aggregates data from multiple external APIs (OpenWeather, NewsAPI, TMDb).
- Allows users to query news, weather, and movie data in a single API call.
- Supports optional query parameters for filtering and sorting the results.
- Handles API keys securely through configuration in the appsettings.json file.
- Includes robust error handling and logging.

**Prerequisites**
API Keys for the following external services:
- OpenWeatherMap (Weather API)
- NewsAPI (News API)
- TMDb (Movies API)
You must set up the respective API keys in the appsettings.json file.

**API Endpoint**
GET /api/aggregate
This endpoint retrieves aggregated data (weather, news, and movies) based on the provided query parameters.

**Request Parameters:**
| Parameter     | Type        | Required | Description                                      |
|---------------|-------------|----------|--------------------------------------------------|
| `Country`     | string      | No       | 2-letter country code (e.g., us, gb, gr)         |
| `NewsCategory`| string      | No       | Category of news (e.g., technology, sports)      |
| `NewsKeyword` | string      | No       | Keyword for searching news articles              |
| `Date`        | DateType    | No       | The date for the query (e.g., 2025-02-07)        |
| `SortBy`      | Enum        | No       | Sorting option (e.g., date, popularity)          |
