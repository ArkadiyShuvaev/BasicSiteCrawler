/// <binding BeforeBuild='default' />

var gulp = require("gulp");
var tsify = require("tsify");
var ts = require("gulp-typescript");
var sourcemaps = require("gulp-sourcemaps");
var source = require('vinyl-source-stream');
var browserify = require("browserify");
var tsProject = ts.createProject("tsconfig.json", {});

var tsSrc = ["ClientApp/index.tsx"];

gulp.task("signalR-client", function () {
    //gulp.src("node_modules/@aspnet/signalr/dist/browser/signalr.min.js")
    //    .pipe(gulp.dest("wwwroot/dist"));
});

gulp.task("default", ["signalR-client"], function () {
    return browserify({
            basedir: '.',
            debug: true,
            entries: [tsSrc],
            cache: {},
            packageCache: {}
        })
        .plugin(tsify)
        .bundle()
        .pipe(source('bundle.js'))
        .pipe(gulp.dest("wwwroot/dist"));
});
