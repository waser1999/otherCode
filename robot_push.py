import requests
import json

def wx_notification(text):
    webhook = 'https://qyapi.weixin.qq.com/cgi-bin/webhook/send?key=2c7eff5f-9de6-48ed-9e2a-6e7d47a4b017'

    header = {
        'Content-Type' : 'application/json'
    }

    body = {
        'msgtype' : 'text',
        'text': {
            'content': text
        }
    }
    resp = requests.post(webhook, headers=header, data=json.dumps(body))

wx_notification('派蒙才不笨，我要给你起一个难听的绰号！')