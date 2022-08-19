import requests
import re
import random
import string
from flask import Flask, jsonify, request


#generate a random 16 digit hex number
def GenerateRandomHex():
    return ''.join(random.choice(string.hexdigits) for _ in range(16))

id = GenerateRandomHex()

def GetJWTToken():  
    url = "https://us-prod.api.mcd.com/v1/security/auth/token"

    payload = "grantType=client_credentials"
    headers = {
        "Host": "us-prod.api.mcd.com",
        "Mcd-Clientid": "8cGckR5wPgQnFBc9deVhJ2vT94WhMBRL",
        "Authorization": "Basic OGNHY2tSNXdQZ1FuRkJjOWRlVmhKMnZUOTRXaE1CUkw6WW00clZ5cXBxTnBDcG1yZFBHSmF0UnJCTUhoSmdyMjY=",
        "Mcd-Clientsecret": "Ym4rVyqpqNpCpmrdPGJatRrBMHhJgr26",
        "Cache-Control": "true",
        "Accept-Charset": "UTF-8",
        "User-Agent": "MCDSDK/23.0.15 (Android; 31; en-US) GMA/7.5.0",
        "Accept-Language": "en-US",
        "Mcd-Sourceapp": "GMA",
        "Mcd-Uuid": "e03e4221-477b-4f8d-ab7f-ba9f21f10963",
        "Mcd-Marketid": "US",
        "Tracestate": "@nr=0-2-734056-436998460-18cfb8f798114a1b----1659985176393",
        "Traceparent": "00-2aa6690ba261445ba08317978d86352f-18cfb8f798114a1b-00",
        "Newrelic": "eyJ2IjpbMCwyXSwiZCI6eyJkLnR5IjoiTW9iaWxlIiwiZC5hYyI6IjczNDA1NiIsImQuYXAiOiI0MzY5OTg0NjAiLCJkLnRyIjoiMmFhNjY5MGJhMjYxNDQ1YmEwODMxNzk3OGQ4NjM1MmYiLCJkLmlkIjoiMThjZmI4Zjc5ODExNGExYiIsImQudGkiOjE2NTk5ODUxNzYzOTN9fQ==",
        "Content-Type": "application/x-www-form-urlencoded",
        "Content-Length": "28",
        "Accept-Encoding": "gzip, deflate",
        "X-Newrelic-Id": "UwUDUVNVGwcDUlhbDwUBVg=="
    }

    response = requests.request("POST", url, data=payload, headers=headers)

    #print(response.text)
    regex = re.compile(r'(?<=token": ").*?(?=")')
    token = regex.findall(response.text)
    return str(token[0])

def RegisterAccount(email, token):
    import requests

    url = "https://us-prod.api.mcd.com/exp/v1/customer/registration"

    payload = {
        "address": {
            "country": "US",
            "zipCode": "84112"
        },
        "audit": {"registrationChannel": "M"},
        "credentials": {
            "loginUsername": email + "@mail.bigmac.social",
            "sendMagicLink": True,
            "type": "email"
        },
        "device": {
            "deviceId": id,
            "deviceIdType": "AndroidId",
            "isActive": "Y",
            "os": "android",
            "osVersion": "12",
            "pushNotificationId": "fTVT0zZgFd4:APA91bGX2VWiuH_zkn2umGY5WWondtNLdoCXjehndHqvvx7bX4JFY-w2-brZIfWcvIEVVZxCpNxGbFmyAqCb5gN3z4daSUQ1ktTljTm5drQTDzeXVeoWPq7359cBe-ou8HKgBKPP1lGE",
            "timezone": "America/Denver"
        },
        "emailAddress": email + "@mail.bigmac.social",
        "firstName": "Alex",
        "lastName": "Whore",
        "optInForMarketing": False,
        "policies": {"acceptancePolicies": {
                "1": True,
                "4": True,
                "5": False,
                "6": False
            }},
        "preferences": [
            {
                "details": {
                    "mobileApp": "en-US",
                    "email": "en-US"
                },
                "preferenceId": 1
            },
            {
                "details": {
                    "mobileApp": "N",
                    "email": "N"
                },
                "preferenceId": 2
            },
            {
                "details": {
                    "mobileApp": "Y",
                    "email": "Y"
                },
                "preferenceId": 3
            },
            {
                "details": {
                    "mobileApp": "Y",
                    "email": "Y"
                },
                "preferenceId": 4
            },
            {
                "details": {
                    "mobileApp": "Y",
                    "email": "Y"
                },
                "preferenceId": 6
            },
            {
                "details": {
                    "mobileApp": "Y",
                    "email": "Y"
                },
                "preferenceId": 7
            },
            {
                "details": {
                    "mobileApp": "Y",
                    "email": "Y"
                },
                "preferenceId": 8
            },
            {
                "details": {
                    "mobileApp": "Y",
                    "email": "Y"
                },
                "preferenceId": 9
            },
            {
                "details": {
                    "mobileApp": "Y",
                    "email": "Y"
                },
                "preferenceId": 10
            },
            {
                "details": {
                    "mobileApp": [4, 5],
                    "email": [1, 2, 3]
                },
                "preferenceId": 11
            },
            {
                "details": {"enabled": "Y"},
                "preferenceId": 12
            },
            {
                "details": {"enabled": "Y"},
                "preferenceId": 13
            },
            {
                "details": {"enabled": "Y"},
                "preferenceId": 14
            },
            {
                "details": {"enabled": "Y"},
                "preferenceId": 15
            },
            {
                "details": {"enabled": "Y"},
                "preferenceId": 16
            },
            {
                "details": {"enabled": "Y"},
                "preferenceId": 17
            },
            {
                "details": {"enabled": "Y"},
                "preferenceId": 18
            },
            {
                "details": {"enabled": "Y"},
                "preferenceId": 19
            },
            {
                "details": {"enabled": "Y"},
                "preferenceId": 20
            },
            {
                "details": {"enabled": "Y"},
                "preferenceId": 21
            },
            {
                "details": {"enabled": "Y"},
                "preferenceId": 22
            }
        ],
        "subscriptions": [
            {
                "optInStatus": "Y",
                "subscriptionId": "1"
            },
            {
                "optInStatus": "Y",
                "subscriptionId": "2"
            },
            {
                "optInStatus": "Y",
                "subscriptionId": "3"
            },
            {
                "optInStatus": "Y",
                "subscriptionId": "4"
            },
            {
                "optInStatus": "Y",
                "subscriptionId": "5"
            },
            {
                "optInStatus": "Y",
                "subscriptionId": "7"
            },
            {
                "optInStatus": "N",
                "subscriptionId": "10"
            },
            {
                "optInStatus": "Y",
                "subscriptionId": "11"
            },
            {
                "optInStatus": "Y",
                "subscriptionId": "24"
            },
            {
                "optInStatus": "Y",
                "subscriptionId": "25"
            }
        ]
    }
    headers = {
        "Host": "us-prod.api.mcd.com",
        "X-Acf-Sensor-Data": "1,a,dDFhvP07iseYfVo9AyjUBb4tvjd8WGtqStnD1hDFSneHDlhQBVq+9/ggV5ncWwswOYE8A6a7+OisNuYYmDR2qDXFPHoeEJE0FTsJfaX6FBjAisbKRmvNu0qeE51nGome8reiu6zZgWlMEl37HHksxcic/MEz8Do5MMMrl7RyFE8=,jguyhIrGLRGR6gggh+cod8ACxzC5Le1EpmD5JSGmDpprKHKxLorFrC0gYl4wzqneNQaXxSKKzlN5i5M0T9x6EeJuitXsxIz8/Dkr70CvLoG+n5YUlH4QO2505JfDo10agOhcwt15I1gAH5HqqWGI38Sownz7TVXZjqd7xoV/ADI=$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$0,0,0",
        "Mcd-Clientid": "8cGckR5wPgQnFBc9deVhJ2vT94WhMBRL",
        "Authorization": "Bearer " + token,
        "Cache-Control": "true",
        "Accept-Charset": "UTF-8",
        "User-Agent": "MCDSDK/23.0.15 (Android; 31; en-US) GMA/7.5.0",
        "Accept-Language": "en-US",
        "Mcd-Sourceapp": "GMA",
        "Mcd-Uuid": "748acfab-e896-43d0-bafe-f584747c06e0",
        "Mcd-Marketid": "US",
        "Traceparent": "00-784d73908f7a47e882a80906d63aa486-2ac4e09d23d84d29-00",
        "Tracestate": "@nr=0-2-734056-436998460-2ac4e09d23d84d29----1659655543559",
        "Newrelic": "eyJ2IjpbMCwyXSwiZCI6eyJkLnR5IjoiTW9iaWxlIiwiZC5hYyI6IjczNDA1NiIsImQuYXAiOiI0MzY5OTg0NjAiLCJkLnRyIjoiNzg0ZDczOTA4ZjdhNDdlODgyYTgwOTA2ZDYzYWE0ODYiLCJkLmlkIjoiMmFjNGUwOWQyM2Q4NGQyOSIsImQudGkiOjE2NTk2NTU1NDM1NTl9fQ==",
        "Content-Type": "application/json; charset=UTF-8",
        "Content-Length": str(2236 + (len(email) * 2)),
        "Accept-Encoding": "gzip, deflate",
        "X-Newrelic-Id": "UwUDUVNVGwcDUlhbDwUBVg=="
    }

    response = requests.request("POST", url, json=payload, headers=headers)

    #print(response.text)
    regex = re.compile(r'(?<=accessToken":").*?(?=")')
    token = regex.findall(response.text)
    return token[0]

    #return str(token[0])


# creating a Flask app
app = Flask(__name__)
  
  
  
@app.route('/', methods = ['GET', 'POST'])
def home():
    if(request.method == 'GET'):
  
        data = "hello world"
        return jsonify({'data': data})
  
  
@app.route('/createAccount/<string:email>', methods = ['GET'])
def disp(email):
    token = GetJWTToken()
    accountToken = RegisterAccount(email, token)
    return jsonify({'token': accountToken})
  
  
# driver function
if __name__ == '__main__':
  
    app.run(host='0.0.0.0', port=5000)