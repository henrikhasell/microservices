var gulp = require('gulp4');

gulp.task('copy', function() {
    return gulp.src([
        'node_modules/bootstrap/dist/css/bootstrap.min.css',
        'node_modules/bootstrap/dist/js/bootstrap.min.js',
        'node_modules/popper.js/dist/popper.min.js',
        'node_modules/jquery/dist/jquery.min.js'
    ]).pipe(gulp.dest('wwwroot'));
});

gulp.task('default', gulp.parallel('copy'));