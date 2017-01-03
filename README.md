# Project PredictLeo
Simply put, Leonardo is a prediction chat bot.

* Leonardo will greet you
* Explains what it does
* Takes inputs from you one at a time on a chat window (Skype or Slack for example)
* Does all the data crunching and spits out the predicted value

# How it works?

There are 2 parts to it, the front end chat client and back end machine learning web service

## 1. Chat Client

The chat client is build using Microsoft Chatbot framework which utilizes .Net framework

## 2. Machine Learning Web Service

The user inputs are collected and converted to JSON format by Leonardo. Then a web hosted Machine Learning model is called using a web serive with the JSON inputs.
The machine learning model then predicts the output and sends it back to Leonardo.

# About the Machine Learning Model

The model and the underlying training data are available on the Azure machine Learning (AML) Studio. Firstly, a training experiment is build and tuned using the available modules on AML.
The training experiment is then converted in to a predictive experiment which is then deployed in to a predictive webservice which can be called using an API key.

# Next Steps

1. Replicating the current framework to more relevant applications. Currenly collecting training data to build models

2. Integrating NLP (Natural Language Processing) in to chat framework, preferably using Microsoft Cognitive APIs

If you are interested to try out the product so far, please ping me. I will send out the link to add Leo in your Skype.

In case you like the work and are willing to partner, I will be glad to have the chat.

-Aditya


