ChargifyDirectSampleDotNet
==========================

This is a small [MVC4](http://www.asp.net/mvc) app that demonstrates how to use [Chargify Direct](http://docs.chargify.com/chargify-direct-introduction) for
Signups.  It leverages the .NET platform to create the Direct secure form inputs and signature, verify the redirect response, and fetch the call response.

Getting Started
---------------

1. Clone this repo to your local machine
2. Edit `web.config` to add your own API User credentials (created on your site, https://<your domain>.chargify.com/settings#chargify-direct)
3. Create products on your API User's Site with handles 'basic' and 'premium' (or edit the example to match product handles you have)
4. Run the app

##Demo##
This code has been deployed to http://chargify-direct-sample.azurewebsites.net/ for your convenience.