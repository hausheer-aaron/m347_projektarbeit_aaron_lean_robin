FROM nginx:alpine

COPY src/bin/Release/net*/publish/wwwroot /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]