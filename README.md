# proliferate

## A work in progress serverless email delivery platform

![AWS](_github/proliferate.png "AWS infra")

French documentation can be found [here](draft.md).

## Codebase

`proliferate` is based on the microservice principles. Please refer to [this](https://serverless.com/blog/api-gateway-multiple-services/) if you want to understand the serverless setup. Each service is living on his own with his associated infrastructure resources. Those services could be written in any language if needed.

[Write Serverless Functions Using AWS Lambda And C#
](https://gooroo.io/GoorooThink/Article/17421/Write-Serverless-Functions-Using-AWS-Lambda-And-C/29348)

## Services

### send-email

This function is the entry point for sending an email through `proliferate`. This is the only function that is exposed through the API gateway.

Input:
```javascript
{
    "api_key": "21e7176c-6051-11e8-9c2d-fa7ae01bbebc",
    "from": "from@example.com",
    "to": [
        "to@example.com"
    ],
    "cc": [
        "cc@example.com"
    ],
    "bcc": [
        "bcc@example.com"
    ],
    "subject": "Example",
    "content": {
        "text": "This is an email",
        "html": "<p>This is an email</p>"
    }
}
```

Output:
```javascript
{
    "Data": null,
    "Errors": [],
    "Warnings": [],
    "Success": true,
    "HasWarnings": false
}
```

#### validate-email

This service is used to validate email. Data normalization is also performed:

* Remove unnecessary whitespaces from email addresses
* Remove invalid characters from email addresses
* Put all the email addresses in lowercase

Ultimately, this function also extracts the domain from email addresses and performs DNS queries to determine if they can be reached.

#### validate-api-key

This service is used to validate API keys. Unallowed API keys will not be able to send email from `proliferate`.

To create a new access key, you must go to the `proliferate-{stage}-api-keys` table and add a name and a key in whatever format you like. This key will then be used throughout the application. We strongly suggest something that cannot be guessed easily. Example:

api_key - `8f0a4306-2e14-420c-bbee-fb2fc1d36777`
client_name - `proliferate-client`

## Documentation

#### Deployment
For `dev` stage:

`sls deploy`

For `prod` stage:

`sls deploy --stage prod`

#### Creating a new service

```bash
sls create --template aws-csharp --name {YOUR_SERVICE_NAME} --path ./src/{YOUR_SERVICE_NAME}
cd src/{YOUR_SERVICE_NAME}
.\build.cmd
sls deploy
sls invoke -f {YOUR_SERVICE_NAME}
```

## Support

Please [open an issue](https://github.com/spektrummedia/proliferate/issues/new) for support.

## Contributing

Please contribute using [Github Flow](https://guides.github.com/introduction/flow/). Create a branch, add commits, and [open a pull request](https://github.com/spektrummedia/proliferate/compare).