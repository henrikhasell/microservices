FROM node AS build
WORKDIR /app
COPY gulpfile.js ./
COPY package.json ./
RUN npm install
RUN ./node_modules/.bin/gulp4
FROM nginx
WORKDIR /usr/share/nginx/html/
COPY --from=build /app/wwwroot static
