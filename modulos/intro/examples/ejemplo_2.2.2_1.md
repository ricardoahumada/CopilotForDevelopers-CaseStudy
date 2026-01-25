# Código Generado por Vibe Coding - Pantalla de Login

**Contexto de uso:** Este código fue generado a partir de un prompt de Vibe Coding, demostrando el resultado típico de este enfoque: código funcional pero sin estructura de proyecto, sin consideraciones de seguridad robustas y sin integración con sistemas existentes.

**Código generado:**
```tsx
import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet } from 'react-native';

const LoginScreen = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [emailError, setEmailError] = useState('');

  const validateEmail = (email) => {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
  };

  const handleLogin = () => {
    if (!validateEmail(email)) {
      setEmailError('Por favor ingresa un email válido');
      return;
    }
    // Lógica de login...
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Bienvenido</Text>
      
      <TextInput
        style={styles.input}
        placeholder="Email"
        value={email}
        onChangeText={(text) => {
          setEmail(text);
          setEmailError('');
        }}
        keyboardType="email-address"
      />
      {emailError ? <Text style={styles.error}>{emailError}</Text> : null}
      
      <TextInput
        style={styles.input}
        placeholder="Password"
        value={password}
        onChangeText={setPassword}
        secureTextEntry
      />
      
      <TouchableOpacity style={styles.button} onPress={handleLogin}>
        <Text style={styles.buttonText}>Iniciar Sesión</Text>
      </TouchableOpacity>
      
      <TouchableOpacity>
        <Text style={styles.forgotPassword}>¿Olvidaste tu contraseña?</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: { flex: 1, justifyContent: 'center', padding: 20, backgroundColor: '#fff' },
  title: { fontSize: 28, fontWeight: 'bold', marginBottom: 30, textAlign: 'center' },
  input: { borderWidth: 1, borderColor: '#ddd', borderRadius: 8, padding: 12, marginBottom: 10 },
  button: { backgroundColor: '#007AFF', padding: 15, borderRadius: 8, marginTop: 10 },
  buttonText: { color: '#fff', textAlign: 'center', fontWeight: 'bold', fontSize: 16 },
  error: { color: '#ff3b30', marginBottom: 10 },
  forgotPassword: { color: '#007AFF', textAlign: 'center', marginTop: 20 }
});

export default LoginScreen;
```

**Problemas identificados:**

| Problema | Descripción |
|----------|-------------|
| **Sin autenticación real** | La lógica de login está incompleta |
| **Validación básica** | Solo valida formato, no robustez |
| **Sin manejo de errores** | No maneja errores de red ni API |
| **Sin tests** | No incluye cobertura de pruebas |
| **Estilos inline** | No sigue patrones de arquitectura |
| **Sin acceso a API** | No conecta con backend real |

**Lección aprendida:** El código de Vibe Coding es útil como punto de partida pero requiere refinamiento significativo antes de producción. SDD aborda esto especificando criterios de aceptación y validaciones desde el inicio.
