MIN_HASH_TABLE_CAPACITY = 3


class Pila:
    def __init__(self):
        self._datos = []

    def apilar(self, valor):
        self._datos.append(valor)

    def desapilar(self):
        if self.esta_vacia():
            raise IndexError("La pila está vacía")
        return self._datos.pop()

    def cima(self):
        if self.esta_vacia():
            raise IndexError("La pila está vacía")
        return self._datos[-1]

    def esta_vacia(self):
        return len(self._datos) == 0

    def tamano(self):
        return len(self._datos)


class TablaHash:
    def __init__(self, capacidad=10):
        self.capacidad = max(MIN_HASH_TABLE_CAPACITY, capacidad)
        self._cubetas = [[] for _ in range(self.capacidad)]

    def _hash(self, clave):
        return hash(clave) % self.capacidad

    def insertar(self, clave, valor):
        indice = self._hash(clave)
        cubeta = self._cubetas[indice]

        for i, (k, _) in enumerate(cubeta):
            if k == clave:
                cubeta[i] = (clave, valor)
                return

        cubeta.append((clave, valor))

    def obtener(self, clave):
        indice = self._hash(clave)
        cubeta = self._cubetas[indice]

        for k, v in cubeta:
            if k == clave:
                return v
        return None

    def eliminar(self, clave):
        indice = self._hash(clave)
        cubeta = self._cubetas[indice]

        for i, (k, v) in enumerate(cubeta):
            if k == clave:
                del cubeta[i]
                return v
        return None

    def items(self):
        for cubeta in self._cubetas:
            for clave, valor in cubeta:
                yield clave, valor


def invertir_texto(texto):
    """Invierte un texto usando una pila con fines de práctica."""
    pila = Pila()
    for caracter in texto:
        pila.apilar(caracter)

    resultado = []
    while not pila.esta_vacia():
        resultado.append(pila.desapilar())
    return "".join(resultado)


def contar_frecuencias(elementos):
    elementos_lista = list(elementos)
    tabla = TablaHash(capacidad=max(MIN_HASH_TABLE_CAPACITY, len(elementos_lista) + 1))
    for elemento in elementos_lista:
        actual = tabla.obtener(elemento) or 0
        tabla.insertar(elemento, actual + 1)

    return {clave: valor for clave, valor in tabla.items()}
