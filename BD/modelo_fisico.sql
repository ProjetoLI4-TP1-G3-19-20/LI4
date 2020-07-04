-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema trabalholi4
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema trabalholi4
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `trabalholi4` DEFAULT CHARACTER SET utf8 ;
USE `trabalholi4` ;

-- -----------------------------------------------------
-- Table `trabalholi4`.`instituicao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trabalholi4`.`instituicao` (
  `id_inst` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NULL DEFAULT NULL,
  `localizacao` VARCHAR(500) NULL DEFAULT NULL,
  PRIMARY KEY (`id_inst`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `trabalholi4`.`departamentos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trabalholi4`.`departamentos` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NULL,
  `id_inst` INT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_departamentos_instituicao1_idx` (`id_inst` ASC) VISIBLE,
  CONSTRAINT `fk_departamentos_instituicao1`
    FOREIGN KEY (`id_inst`)
    REFERENCES `trabalholi4`.`instituicao` (`id_inst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `trabalholi4`.`visitante`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trabalholi4`.`visitante` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `Telemóvel` VARCHAR(20) NULL DEFAULT NULL,
  `Nome` VARCHAR(100) NULL DEFAULT NULL,
  `email` VARCHAR(100) NULL DEFAULT NULL,
  `morada` VARCHAR(300) NULL DEFAULT NULL,
  `cod_postal` VARCHAR(8) NULL DEFAULT NULL,
  `password` VARCHAR(200) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `trabalholi4`.`pedidovisita`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trabalholi4`.`pedidovisita` (
  `hora_inicio` DATETIME NOT NULL,
  `hora_fim` DATETIME NULL,
  `comentarios` VARCHAR(500) NULL DEFAULT NULL,
  `idInst` INT NULL DEFAULT NULL,
  `idDepartamento` INT NULL DEFAULT NULL,
  `visitado` VARCHAR(100) NULL DEFAULT NULL,
  `idVisita` INT NOT NULL AUTO_INCREMENT,
  `idUser` INT NOT NULL,
  PRIMARY KEY (`idVisita`),
  INDEX `inst_idx` (`idInst` ASC) VISIBLE,
  INDEX `user_idx` (`idUser` ASC) VISIBLE,
  CONSTRAINT `inst`
    FOREIGN KEY (`idInst`)
    REFERENCES `trabalholi4`.`instituicao` (`id_inst`),
  CONSTRAINT `user`
    FOREIGN KEY (`idUser`)
    REFERENCES `trabalholi4`.`visitante` (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `trabalholi4`.`pessoadeinteresse`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trabalholi4`.`pessoadeinteresse` (
  `nome` VARCHAR(100) NOT NULL,
  `email` VARCHAR(45) NULL DEFAULT NULL,
  `departamentos_id` INT NOT NULL,
  `departamentos_id_inst` INT NOT NULL,
  `password` VARCHAR(200) NULL,
  `phone` VARCHAR(45) NULL,
  PRIMARY KEY (`nome`, `departamentos_id`, `departamentos_id_inst`),
  INDEX `fk_pessoadeinteresse_departamentos1_idx` (`departamentos_id` ASC, `departamentos_id_inst` ASC) VISIBLE,
  CONSTRAINT `fk_pessoadeinteresse_departamentos1`
    FOREIGN KEY (`departamentos_id`)
    REFERENCES `trabalholi4`.`departamentos` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `trabalholi4`.`pessoadeinteresse_has_vagas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trabalholi4`.`pessoadeinteresse_has_vagas` (
  `nome` VARCHAR(100) NOT NULL,
  `hora_inicio` DATETIME NOT NULL,
  `hora_fim` DATETIME NOT NULL,
  PRIMARY KEY (`nome`, `hora_inicio`, `hora_fim`),
  INDEX `fk_PessoaDeInteresse_has_Vagas_PessoaDeInteresse1_idx` (`nome` ASC) VISIBLE,
  CONSTRAINT `fk_PessoaDeInteresse_has_Vagas_PessoaDeInteresse1`
    FOREIGN KEY (`nome`)
    REFERENCES `trabalholi4`.`pessoadeinteresse` (`nome`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `trabalholi4`.`trabalhadores`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trabalholi4`.`trabalhadores` (
  `Nome` VARCHAR(45) NULL DEFAULT NULL,
  `Telemovel` VARCHAR(20) NULL DEFAULT NULL,
  `email` VARCHAR(100) NULL DEFAULT NULL,
  `id_inst` INT NOT NULL,
  `admin` TINYINT NULL DEFAULT NULL,
  `id_col` INT NOT NULL AUTO_INCREMENT,
  `password` VARCHAR(200) NULL,
  PRIMARY KEY (`id_col`),
  INDEX `fk_Colaboradores_instituições1_idx` (`id_inst` ASC) VISIBLE,
  CONSTRAINT `fk_Colaboradores_instituições1`
    FOREIGN KEY (`id_inst`)
    REFERENCES `trabalholi4`.`instituicao` (`id_inst`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `trabalholi4`.`visitas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `trabalholi4`.`visitas` (
  `idInstituicao` INT NOT NULL,
  `idUser` INT NOT NULL,
  `dataInicio` DATETIME NOT NULL,
  `dataSaida` DATETIME NOT NULL,
  `estado` INT NULL DEFAULT NULL,
  `avaliacao` VARCHAR(100) NULL DEFAULT NULL,
  `comentarios` VARCHAR(500) NULL DEFAULT NULL,
  `visitado` VARCHAR(100) NULL,
  `departamentos_id` INT NOT NULL,
  PRIMARY KEY (`idInstituicao`, `idUser`, `dataInicio`, `departamentos_id`),
  INDEX `fk_Instituicoes_has_Visita_Instituicoes1_idx` (`idInstituicao` ASC) VISIBLE,
  INDEX `fk_Visitas_Users1` (`idUser` ASC) VISIBLE,
  INDEX `fk_visitas_departamentos1_idx` (`departamentos_id` ASC) VISIBLE,
  CONSTRAINT `fk_Instituicoes_has_Visita_Instituicoes1`
    FOREIGN KEY (`idInstituicao`)
    REFERENCES `trabalholi4`.`instituicao` (`id_inst`),
  CONSTRAINT `fk_Visitas_Users1`
    FOREIGN KEY (`idUser`)
    REFERENCES `trabalholi4`.`visitante` (`id`),
  CONSTRAINT `fk_visitas_departamentos1`
    FOREIGN KEY (`departamentos_id`)
    REFERENCES `trabalholi4`.`departamentos` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
