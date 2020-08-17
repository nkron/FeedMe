# FeedMe

Check out the site at [Feedme.fit](http://feedme.fit)

FeedMe is a meal planning app designed to help those who want easy, basic plans filled with things they already know how to make.
Other meal plan generators suggest such a wide variety of meals that it often feels like you have to become a chef on the side just to feed yourself.

In its current state this is a complete meal planning app with the ability to search global databases for food, define your own foods, and track calories vs your own set goals. It can even help you calculate the calories and macronutrients you should aim for according to your goals!

Next steps are to expand the scope to include meals composed of individual foods, and integrating meal apis in addition to food apis. Then the meal generator will be able to be completed.

## Features
* Track the foods you eat for every meal
* Calculate detailed nutrition guidelines based on your goals
* Create your own custom foods. Your foods are available for others to use as well!
* Search a global database for common branded foods and customize them.

## Design
FeedMe uses a .net core c# MVC back-end, Microsoft SQL Server db, and a bootstrap/javascript/jquery front-end.

Everything is hosted in the cloud on my personal Azure.

Foods are retrieved from the [Edamam Food API](https://developer.edamam.com/food-database-api-docs) and combined with SQL Server data.

*Some best practices are ignored for the sake of time - ideally all classes would have their own interfaces for example

## Planned Features
* Favoriting foods
* Food search giving you your recent history as default
* Integrate Edamam Meal API and build out meals in the MVC backend
* Meal generation algorithm
