﻿angular
    .module('materialsApp.ctrl.new', ['ngResource'])
    .controller('newCtrl', [
        '$scope',
        "$rootScope",
        '$location',
        '$compile',
        '$resource',
        "materialsService",
        function ($scope, $rootScope, $location, $compile, $resource, materialsService) {

       
            $scope.$watch('user', function () {


                if ($scope.user == 'lector') {
                   var readonly = 0;
                }
                else if ($scope.user == 'student') {
                   var readonly = 1;
                }

                var height = angular.element('.wrapper').height() - angular.element('.header').height() - 55;
                $(".sidebar-menu").empty();


                tinymce.init({
                    save_enablewhendirty: true,
                    setup: function (ed) {

                        var setup = {
                            updateBookContent: function () {

                                var mar = $(ed.getBody()).find('h1,h2,h3,h4,h5,h6');
                                var marker = mar.clone();
                                $(".sidebar-menu").empty();
                                marker.each(function (item) {
                                    $(".sidebar-menu").append("<li>" + $(this)[0].outerHTML + "</li>");
                                })

                                $(".sidebar-menu > li").on("click", function () {
                                    console.log($(this).index());
                                    ed.selection.select(mar.get($(this).index())).scrollIntoView();
                                })
                            },
                            data: null
                        }

                        ed.on('init', function () {
                            materialsService.getText({ id: $scope.$parent.document }).success(function (data) {
                                setup.data = data;
                                ed.setContent(data.Document.Text);
                                setup.updateBookContent();
                            });
                        });

                        ed.on("keyup", function (e) {
                            if (e.keyCode == 13 || e.keyCode == 8 || e.keyCode == 46) {
                                setup.updateBookContent();
                            }
                        });

                        ed.on("click", function (e) {
                                setup.updateBookContent();
                        });

                        //ed.addMenuItem('save_exit', {
                        //    text: 'Сохранить и выйти',
                        //    context: 'file',
                        //    onclick: function (editor) {
                        //        editor.save();
                        //        $location.url("/Catalog");
                        //        $scope.$apply();
                        //    }
                        //});
                    
                        ed.addMenuItem('exit', {
                            text: 'Выход',
                            context: 'file',
                            onclick: function () {
                                $(".sidebar-menu").empty();
                                $location.url("/Catalog");
                                angular.element("#headerMainPage").show();
                                $(".sidebar-menu").append($compile('<li><a href="/Lms" class="linkNavbar"><i class="fa fa-reply fa-tab"></i>В главное меню</a></li><li style="cursor:pointer;" ng-click="backspaceFolder()" ><a class="linkNavbar"><i class="fa fa-reply fa-tab"></i>Назад</a></li>')($rootScope));
                                $scope.$apply();
                            }
                        });


                    },
                    save_onsavecallback: function (dr) {
                        saveText(dr);
                    },
                    selector: "textarea",
                    autoresize_min_height: height+"px",
                    language: 'ru',
                    skin: 'light',
                    plugins: [
                        "pagebreak save advlist autolink lists link image charmap print preview anchor",
                        "searchreplace visualblocks code fullscreen",
                        "insertdatetime media table contextmenu paste autoresize"
                    ],
                    pagebreak_separator: "<!-- my page break -->",
                    menubar: true,
                    readonly: readonly,
                    toolbar: "save | insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image"
                });

            });
            

            function getParameterByName(name) {
                name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                    results = regex.exec(location.search);
                return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
            }

            var subjectId = getParameterByName("subjectId");

            angular.element("#headerMainPage").hide();


            function saveText(obj) {
                var text = obj.getContent();
                data = {
                    idd: $scope.$parent.document || 0,
                    idf: $scope.$parent.folder || 0,
                    name: $scope.$parent.nameDocument || "Новый документ",
                    text: text,
                    subjectId: subjectId
                }
                materialsService.saveText(data).success(function (data) {
                    //$scope.folders = data.Folders;
                });
            }

        }]);


    
    
