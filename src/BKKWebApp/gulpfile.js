const { src, dest, parallel, series } = require('gulp');
const rename = require("gulp-rename");

var sass = require('gulp-sass');

sass.compiler = require('node-sass');


function build(done) {
    return src("websrc/app/css/default.scss")
        .pipe(sass().on('error', sass.logError))
        .pipe(rename(function (path) {
            path.basename = "main";
          }))
        .pipe(dest('wwwroot/app/css/'));
}

exports.default = build;