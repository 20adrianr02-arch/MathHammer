import 'package:flutter/material.dart';

import 'pantallas/pantalla_combate.dart';

void main() {
  runApp(const MathHammerApp());
}

class MathHammerApp extends StatelessWidget {
  const MathHammerApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'MathHammer',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        useMaterial3: true,
        brightness: Brightness.dark,
        scaffoldBackgroundColor: const Color(0xFF030712),
        colorScheme: ColorScheme.fromSeed(
          seedColor: const Color(0xFFc3272b),
          brightness: Brightness.dark,
          surface: const Color(0xFF111416),
        ),
      ),
      home: const PantallaCombate(),
    );
  }
}