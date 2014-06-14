﻿var knowledgeTestingApp = angular.module('knowledgeTestingApp', ['ngRoute', 'ui.bootstrap']);

knowledgeTestingApp.config(function ($routeProvider) {

    $routeProvider.when("/tests", {
        controller: "testsCtrl",
        templateUrl: "/Content/KnowledgeTesting/tests.html"
    });
    
    $routeProvider.when("/control", {
        controller: "passingCtrl",
        templateUrl: "/Content/KnowledgeTesting/control.html"
    });
    
    $routeProvider.when("/results", {
        controller: "resultsCtrl",
        templateUrl: "/Content/KnowledgeTesting/results.html"
    });
    
    $routeProvider.when("/questions", {
        controller: "questionsCtrl",
        templateUrl: "/Content/KnowledgeTesting/questions.html"
    });

    $routeProvider.otherwise({ redirectTo: "/tests" });

});