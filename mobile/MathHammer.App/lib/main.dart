import 'package:flutter/material.dart';

import 'pantallas/pantalla_combate.dart';
import 'temas/tema.dart';

void main() {
  runApp(const MathHammerApp());
}

class MathHammerApp extends StatefulWidget {
  const MathHammerApp({super.key});

  @override
  State<MathHammerApp> createState() => _MathHammerAppState();
}

class _MathHammerAppState extends State<MathHammerApp> {
  TemaMathHammer _tema = listaTemas.first;

  void _cambiarTema(NombreTema nombre) {
    setState(() {
      _tema = listaTemas.firstWhere((tema) => tema.id == nombre);
    });
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'MathHammer',
      debugShowCheckedModeBanner: false,
      theme: construirTemaMaterial(_tema),
      home: TemaAlcance(
        tema: _tema,
        child: PantallaCombate(alCambiarTema: _cambiarTema),
      ),
    );
  }
}