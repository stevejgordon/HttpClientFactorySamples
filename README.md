# HttpClientFactory Samples

A collection of sample applications which demonstrate some potential requirements that may occur when using HttpClient and HttpClientFactory.

## 1. Selective Handler Invocation

In this sample I demonstrate how HttpRequestMessage.Properties can be used to selectively run code within a handler, derived from DelegatingHandler.

[Project Shortcut](/tree/master/src/SelectiveHandlerInvocation)

Blog post coming soon!

## 2. Helper Extension for Registering Delegating Handlers

In this sample I demonstrate a helper method which provides support for adding a handler using HttpClientFactory while also ensuring it is registered into D.I. This simplifies the use of handlers with HttpClientFactory. By default with HttpClientFactory you are required to register the handler into D.I. and also add it to your client(s).

[Project Shortcut](/tree/master/src/DelegatingHandlerRegistrationHelper)

Blog post coming soon!