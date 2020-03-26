-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`Visitante`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Visitante` (
  `id` INT NOT NULL,
  `Telemóvel` VARCHAR(10) NULL,
  `Nome` VARCHAR(100) NULL,
  `e-mail` VARCHAR(45) NULL,
  `morada` VARCHAR(45) NULL,
  `cod_postal` VARCHAR(8) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Instituicao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Instituicao` (
  `id_inst` INT NOT NULL,
  `Nome` VARCHAR(45) NULL,
  `e-mail` VARCHAR(45) NULL,
  `localizacao` VARCHAR(50) NULL,
  PRIMARY KEY (`id_inst`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Departamentos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Departamentos` (
  `id` INT NOT NULL,
  `id_inst` INT NOT NULL,
  `nome` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Departamentos_instituições1_idx` (`id_inst` ASC) VISIBLE,
  CONSTRAINT `fk_Departamentos_instituições1`
    FOREIGN KEY (`id_inst`)
    REFERENCES `mydb`.`Instituicao` (`id_inst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Trabalhadores`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Trabalhadores` (
  `Nome` VARCHAR(45) NULL,
  `Telemovel` VARCHAR(10) NULL,
  `e-mail` VARCHAR(45) NULL,
  `id_inst` INT NOT NULL,
  `admin` TINYINT(2) NULL,
  `id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Colaboradores_instituições1_idx` (`id_inst` ASC) VISIBLE,
  CONSTRAINT `fk_Colaboradores_instituições1`
    FOREIGN KEY (`id_inst`)
    REFERENCES `mydb`.`Instituicao` (`id_inst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Visitas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Visitas` (
  `idInstituicao` INT NOT NULL,
  `idUser` INT NOT NULL,
  `dataInicio` DATETIME NOT NULL,
  `dataSaida` DATETIME NOT NULL,
  `estado` INT NULL,
  `avalicao` VARCHAR(100) NULL,
  `comentarios` VARCHAR(500) NULL,
  INDEX `fk_Instituicoes_has_Visita_Instituicoes1_idx` (`idInstituicao` ASC) VISIBLE,
  PRIMARY KEY (`idInstituicao`, `idUser`, `dataInicio`),
  CONSTRAINT `fk_Instituicoes_has_Visita_Instituicoes1`
    FOREIGN KEY (`idInstituicao`)
    REFERENCES `mydb`.`Instituicao` (`id_inst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Visitas_Users1`
    FOREIGN KEY (`idUser`)
    REFERENCES `mydb`.`Visitante` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Vagas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Vagas` (
  `idDepartamento` INT NOT NULL,
  `HorarioInicio` DATETIME NOT NULL,
  `HorarioFinal` DATETIME NOT NULL,
  `NrDeVagas` INT NULL,
  INDEX `fk_Vagas_Departamentos1_idx` (`idDepartamento` ASC) VISIBLE,
  PRIMARY KEY (`HorarioInicio`, `HorarioFinal`),
  CONSTRAINT `fk_Vagas_Departamentos1`
    FOREIGN KEY (`idDepartamento`)
    REFERENCES `mydb`.`Departamentos` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Contacto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Contacto` (
  `telemovel` VARCHAR(10) NOT NULL,
  `id_inst` INT NOT NULL,
  PRIMARY KEY (`telemovel`),
  INDEX `fk_Contacto_Instituicao1_idx` (`id_inst` ASC) VISIBLE,
  CONSTRAINT `fk_Contacto_Instituicao1`
    FOREIGN KEY (`id_inst`)
    REFERENCES `mydb`.`Instituicao` (`id_inst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`PedidoVisita`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`PedidoVisita` (
  `data` DATETIME NOT NULL,
  `comentarios` VARCHAR(500) NULL,
  `idDepartament0` INT NULL,
  `visitado` VARCHAR(100) NULL,
  `idInst` INT NULL,
  `idVisita` INT NOT NULL,
  `idUser` INT NOT NULL,
  PRIMARY KEY (`idVisita`, `idUser`),
  INDEX `inst_idx` (`idInst` ASC) VISIBLE,
  INDEX `user_idx` (`idUser` ASC) VISIBLE,
  CONSTRAINT `inst`
    FOREIGN KEY (`idInst`)
    REFERENCES `mydb`.`Instituicao` (`id_inst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `user`
    FOREIGN KEY (`idUser`)
    REFERENCES `mydb`.`Visitante` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`PessoaDeInteresse`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`PessoaDeInteresse` (
  `nome` VARCHAR(100) NOT NULL,
  `idInst` INT NOT NULL,
  `email` VARCHAR(45) NULL,
  PRIMARY KEY (`nome`),
  INDEX `fk_PessoaDeInteresse_Instituicao1_idx` (`idInst` ASC) VISIBLE,
  CONSTRAINT `fk_PessoaDeInteresse_Instituicao1`
    FOREIGN KEY (`idInst`)
    REFERENCES `mydb`.`Instituicao` (`id_inst`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`HorariosOcupados`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`HorariosOcupados` (
  `data_inicio` DATETIME NOT NULL,
  `data_fim` DATETIME NULL,
  `nome` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`data_inicio`, `nome`),
  INDEX `pdi_idx` (`nome` ASC) VISIBLE,
  CONSTRAINT `pdi`
    FOREIGN KEY (`nome`)
    REFERENCES `mydb`.`PessoaDeInteresse` (`nome`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`PessoaDeInteresse_has_Vagas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`PessoaDeInteresse_has_Vagas` (
  `nome` VARCHAR(100) NOT NULL,
  `hora_inicio` DATETIME NOT NULL,
  `hora_fim` DATETIME NOT NULL,
  PRIMARY KEY (`nome`, `hora_inicio`, `hora_fim`),
  INDEX `fk_PessoaDeInteresse_has_Vagas_Vagas1_idx` (`hora_inicio` ASC, `hora_fim` ASC) VISIBLE,
  INDEX `fk_PessoaDeInteresse_has_Vagas_PessoaDeInteresse1_idx` (`nome` ASC) VISIBLE,
  CONSTRAINT `fk_PessoaDeInteresse_has_Vagas_PessoaDeInteresse1`
    FOREIGN KEY (`nome`)
    REFERENCES `mydb`.`PessoaDeInteresse` (`nome`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_PessoaDeInteresse_has_Vagas_Vagas1`
    FOREIGN KEY (`hora_inicio` , `hora_fim`)
    REFERENCES `mydb`.`Vagas` (`HorarioInicio` , `HorarioFinal`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
