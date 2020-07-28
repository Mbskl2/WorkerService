
################## REPLACE THESE CONSTANTS #########################
API_ENDPOINT = 'https://example.azurewebsites.net'
AUTH_ZERO_DOMAIN = 'example.eu.auth0.com'
AUTH_ZERO_CLIENT_ID = 'l9CfTyTsB9IcEXAMPLEcINXs06NZ7e7I'
AUTH_ZERO_API_ID = 'https://exampleapi.com'
GOOGLE_LOCATION_API_KEY = 'AIzaSyCdO0mO4IcEXAMPLEcI8sMpdJDQKOAXYLFHM'
####################################################################

def replace_placeholders(content):
    content = content.replace('API_ENDPOINT', API_ENDPOINT)
    content = content.replace('AUTH_ZERO_DOMAIN', AUTH_ZERO_DOMAIN)
    content = content.replace('AUTH_ZERO_CLIENT_ID', AUTH_ZERO_CLIENT_ID)
    content = content.replace('AUTH_ZERO_API_ID', AUTH_ZERO_API_ID)
    content = content.replace('GOOGLE_LOCATION_API_KEY', GOOGLE_LOCATION_API_KEY)
    return content

def replace_settings(config_files):
    for config_file in config_files:
        content = ''
        with open(config_file, 'r') as in_file:
            content = in_file.read()
            content = replace_placeholders(content)
        with open(config_file, 'w') as out_file:
            out_file.write(content)
    
def main():
    config_files = [
        'appsettings.json',
        'ClientApp\src\environments\environment.prod.ts'
    ]
    replace_settings(config_files)

if __name__ == "__main__":
    main()
