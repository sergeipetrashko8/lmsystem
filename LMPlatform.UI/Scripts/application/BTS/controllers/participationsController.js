﻿angular
    .module('btsApp.ctrl.participations', ['ngTable', 'btsApp.service.participations'])
    .controller('participationsCtrl', [
        '$scope',
        'PAGE_SIZE',
        'NgTableParams',
        'participationsService',
        function ($scope, PAGE_SIZE, NgTableParams, participationsService) {
            $scope.lecturers = [];
            $scope.selectedLecturer = null;

            function init() {
                setLecturers();
            };

            function setLecturers() {
                participationsService.getLecturers().then(function (response) {
                    $scope.lecturers = response.data.Lectors;
                    $scope.selectedLecturer = $scope.lecturers[0];
                    $scope.lecturerProjectsTableParams.reload();
                });
            };

            $scope.onChangeGroup = function () {
                $scope.lecturerProjectsTableParams.reload();
            };

            $scope.lecturerProjectsTableParams = new NgTableParams({
                count: PAGE_SIZE
            }, {
                getData: function (params) {
                    if ($scope.selectedLecturer == null)
                        return;
                    return participationsService.getUserProjectsParticipations($scope.selectedLecturer.LectorId, params.page(), params.count(), params.orderBy()).then(function (response) {
                        params.total(response.data.TotalCount);
                        return response.data.Projects;
                    });
                }
            });

            init();
        }]);
