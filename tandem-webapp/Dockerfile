# Build the web app
FROM node:25-alpine AS build
WORKDIR /app

COPY . .
RUN npm ci
RUN npm run build

# Copy the web app into a minimal container
FROM alpine:3.22.2
WORKDIR /frontend-dist
COPY --from=build /app/dist ./
