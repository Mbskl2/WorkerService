# WorkerService

## Specification:
1. API allows anyone with “user” auth0 role to:
  - create a worker profile with name, address and a list of skills,
  - see the workers that they created,
  - modify the workers that they created.
2. API allows anyone with “admin” auth0 role to:
  - create a worker profile with name, address and a list of skills,
  - see all the workers,
  - modify all the workers,
  - search for workers that have a list of skills,
  - search for workers that live in a radius from a specified address.
3. SPA (Angular)

## Deployment steps:
1. Open the solution in Visual Studio:
  - Right click on Worker.Api project and click publish.
  - Create a publishing profile to “Azure App Service Container”.
  - Copy “ Site URL ”. You’re going to need it later.
2. Log into your AuthZero:
  - Create an API with RS256 algorithm.
  - Copy “ Identifier ” which will be later used for audience parameter.
  - Enable “Enable RBAC”.
  - Enable “Add Permissions in the Access Token”.
  - In the permissions tab add the following permissions:
    - create:workers,
    - search:workers,
    - modify:worker,
    - readOwn:workers,
    - readAll:workers.
  - Go to “Users and Roles” -> Roles and create the following 2:
    - admin - with all the permissions for the created API,
    - user - with create, modify and readOwn permissions.
  - Create a SPA Application.
  - Copy “ Domain ” and “ Client ID ”.
  - Paste “ Site URL ” into:
    - Allowed Callback URLs,
    - Allowed Logout URLs,
    - Allowed Web Origins.
3. Prepare your “ GoogleApiKey ” that allows you to use Google Geocoding.
4. In the Worker.Api project there is a python script replace_constants.py. Open in and set:
  - API_ENDPOINT to “ Site URL ”
  - AUTH_ZERO_DOMAIN to “ Domain ”
  - AUTH_ZERO_CLIENT_ID to “ Client ID ”
  - AUTH_ZERO_API_ID to “ Identifier ”
  - GOOGLE_LOCATION_API_KEY to “ GoogleApiKey ”
5. Run the script from cmd: python replace_constants.py
6. Go back to Visual Studio and click “Publish”.
7. Visual Studio should redirect you to the created website.

## Documentation notes:
1. The Api documentation is available at /swagger url.
2. Health check is available at /hc, but it’s always “Healthy”.
3. There is a log file called events.log in the docker.
4. The used db is SQLite with EF Core. It’s called blogging.db and is in the docker.
5. CORS is configured so it accepts all the origins.
6. Polly is used to implement resilient communication with Geocoding api.

## Quick Guide to the SPA:
1. Go to “ Site URL ”.
2. Click “Log In”.
3. Sign up for an account on the Auth0 screen.
4. Go to Auth0 and assign “user” or “admin” role to the account.
5. You’re now at the Home page.
6. Go to “Workers”. Here you’re able to see the list of all workers visible to you.
7. Click “New worker” to fill the form to create a new worker.
8. You can also click one of the workers on the list to see the form with its information to
modify it.
9. To add a new skill fill the “New skill” field and click “Add”.
10. In the field “Country ISO code” provide the iso code i.e. “CH” for Switzerland. Putting the
wrong value may render the “Search by location” functionality temporarily unusable.
