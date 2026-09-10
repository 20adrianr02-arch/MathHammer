import 'package:flutter/material.dart';

/// Temas de color de MathHammer (equivalentes a los de la web).
enum NombreTema { rojo, amarillo, azul, verde, negro }

/// Conjunto de colores de acento de un tema.
class TemaMathHammer {
  const TemaMathHammer({
    required this.id,
    required this.etiqueta,
    required this.colorMuestra,
    required this.acento,
    required this.acentoFuerte,
    required this.acentoClaro,
    required this.acentoOscuro,
    required this.acentoMarca,
    required this.acentoBrillo,
    required this.acentoNiebla,
  });

  final NombreTema id;
  final String etiqueta;
  final Color colorMuestra;
  final Color acento;
  final Color acentoFuerte;
  final Color acentoClaro;
  final Color acentoOscuro;
  final Color acentoMarca;
  final Color acentoBrillo;
  final Color acentoNiebla;
}

const Color fondo = Color(0xFF030712);
const Color panelOscuro = Color(0xFF111416);
const Color bordeCampo = Color(0xFF484d4e);
const Color bordeCabecera = Color(0xFF3b4042);
const Color textoClaro = Color(0xFFf3f4f6);
const Color textoMedio = Color(0xFFcbd5e1);
const Color textoSuave = Color(0xFF9ca3af);
const Color textoTenue = Color(0xFF6b7280);

final List<TemaMathHammer> listaTemas = [
  const TemaMathHammer(
    id: NombreTema.rojo,
    etiqueta: 'MEPHISTON RED',
    colorMuestra: Color(0xFFb3242a),
    acento: Color(0xFFb3242a),
    acentoFuerte: Color(0xFFe05256),
    acentoClaro: Color(0xFFf27b80),
    acentoOscuro: Color(0xFF8a1c21),
    acentoMarca: Color(0xFFb3242a),
    acentoBrillo: Color(0x47B3242A),
    acentoNiebla: Color(0x1CC3272B),
  ),
  const TemaMathHammer(
    id: NombreTema.amarillo,
    etiqueta: 'AVERLAND SUNSET',
    colorMuestra: Color(0xFFe8a33d),
    acento: Color(0xFFe8a33d),
    acentoFuerte: Color(0xFFf5c26b),
    acentoClaro: Color(0xFFfbd795),
    acentoOscuro: Color(0xFFb97a1f),
    acentoMarca: Color(0xFFe8a33d),
    acentoBrillo: Color(0x47E8A33D),
    acentoNiebla: Color(0x1CE8A33D),
  ),
  const TemaMathHammer(
    id: NombreTema.azul,
    etiqueta: 'MACRAGGE BLUE',
    colorMuestra: Color(0xFF1f4690),
    acento: Color(0xFF1f4690),
    acentoFuerte: Color(0xFF3f6bc0),
    acentoClaro: Color(0xFF6f93d6),
    acentoOscuro: Color(0xFF142f63),
    acentoMarca: Color(0xFF1f4690),
    acentoBrillo: Color(0x4D1F4690),
    acentoNiebla: Color(0x221F4690),
  ),
  const TemaMathHammer(
    id: NombreTema.verde,
    etiqueta: 'WAAAGH! FLESH',
    colorMuestra: Color(0xFF3dff2e),
    acento: Color(0xFF3dff2e),
    acentoFuerte: Color(0xFF6bff5e),
    acentoClaro: Color(0xFF9dff94),
    acentoOscuro: Color(0xFF15803d),
    acentoMarca: Color(0xFF16a34a),
    acentoBrillo: Color(0x4716A34A),
    acentoNiebla: Color(0x1A3DFF2E),
  ),
  const TemaMathHammer(
    id: NombreTema.negro,
    etiqueta: 'ABADDON BLACK',
    colorMuestra: Color(0xFF1a1d21),
    acento: Color(0xFF6b7280),
    acentoFuerte: Color(0xFF9ca3af),
    acentoClaro: Color(0xFFcbd5e1),
    acentoOscuro: Color(0xFF374151),
    acentoMarca: Color(0xFF4b5563),
    acentoBrillo: Color(0x406B7280),
    acentoNiebla: Color(0x1C787882),
  ),
];

/// Da acceso al tema activo desde cualquier widget del árbol.
class TemaAlcance extends InheritedWidget {
  const TemaAlcance({super.key, required this.tema, required super.child});

  final TemaMathHammer tema;

  static TemaMathHammer de(BuildContext context) {
    final alcance = context.dependOnInheritedWidgetOfExactType<TemaAlcance>();
    return alcance?.tema ?? listaTemas.first;
  }

  @override
  bool updateShouldNotify(TemaAlcance anterior) => anterior.tema != tema;
}

/// Estilo con la fuente de títulos (Cinzel) y el peso indicado.
TextStyle cinzel({
  required double tamano,
  double peso = 800,
  required Color color,
  double? espaciado,
}) {
  return TextStyle(
    fontFamily: 'Cinzel',
    fontSize: tamano,
    color: color,
    letterSpacing: espaciado,
    fontVariations: [FontVariation('wght', peso)],
  );
}

/// Estilo con la fuente de cuerpo (Inter) y el peso indicado.
TextStyle inter({
  required double tamano,
  double peso = 400,
  required Color color,
  double? espaciado,
}) {
  return TextStyle(
    fontFamily: 'Inter',
    fontSize: tamano,
    color: color,
    letterSpacing: espaciado,
    fontVariations: [FontVariation('wght', peso)],
  );
}

ThemeData construirTemaMaterial(TemaMathHammer tema) {
  return ThemeData(
    useMaterial3: true,
    brightness: Brightness.dark,
    scaffoldBackgroundColor: fondo,
    fontFamily: 'Inter',
    colorScheme: ColorScheme.fromSeed(
      seedColor: tema.acento,
      brightness: Brightness.dark,
      surface: panelOscuro,
    ),
  );
}