/// <binding BeforeBuild='default' />

var gulp = require("gulp");
var tsify = require("tsify");
var ts = require("gulp-typescript");
var sourcemaps = require("gulp-sourcemaps");
var source = require('vinyl-source-stream');
var browserify = require("browserify");
var tsProject = ts.createProject("tsconfig.json", {});

var tsSrc = ["ClientApp/index.tsx"];

//gulp.task("default", function () {
//    return tsProject.src()
//        .pipe(tsProject())
//        .bun
//        .js.pipe(gulp.dest("wwwroot"));
//});

gulp.task("default", function () {
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
