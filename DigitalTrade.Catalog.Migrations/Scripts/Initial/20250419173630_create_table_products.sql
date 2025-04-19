CREATE SCHEMA IF NOT EXISTS catalog;

CREATE TABLE catalog.products
(
    id          BIGSERIAL PRIMARY KEY,
    name        TEXT           NOT NULL,
    category    TEXT[]         NOT NULL,
    description TEXT           NOT NULL,
    image_file  TEXT           NOT NULL,
    price       NUMERIC(18, 2) NOT NULL
);