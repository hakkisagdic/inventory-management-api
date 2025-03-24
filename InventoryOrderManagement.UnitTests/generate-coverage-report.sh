#!/bin/bash

# Test projesi dizinine git
cd "$(dirname "$0")"

# Testleri çalıştır ve kod kapsama raporu oluştur
dotnet test --collect:"XPlat Code Coverage"

# En son oluşturulan kapsama dosyasını bul
COVERAGE_FILE=$(find ./TestResults -name "coverage.cobertura.xml" | sort -r | head -n 1)

if [ -z "$COVERAGE_FILE" ]; then
    echo "Kapsama raporu dosyası bulunamadı."
    exit 1
fi

echo "Kapsama raporu dosyası: $COVERAGE_FILE"

# HTML rapor oluşturma dizini
REPORT_DIR="./TestResults/CoverageReport"
mkdir -p "$REPORT_DIR"

# HTML rapor oluştur
dotnet reportgenerator \
    -reports:"$COVERAGE_FILE" \
    -targetdir:"$REPORT_DIR" \
    -reporttypes:Html

echo "Kapsama raporu HTML formatında oluşturuldu: $REPORT_DIR/index.html"

# İşletim sistemine göre raporu aç
if [ "$(uname)" == "Darwin" ]; then
    open "$REPORT_DIR/index.html"
elif [ "$(expr substr $(uname -s) 1 5)" == "Linux" ]; then
    xdg-open "$REPORT_DIR/index.html"
elif [ "$(expr substr $(uname -s) 1 10)" == "MINGW32_NT" ] || [ "$(expr substr $(uname -s) 1 10)" == "MINGW64_NT" ]; then
    start "$REPORT_DIR/index.html"
else
    echo "Kapsama raporu şu konumda görüntülenebilir: $REPORT_DIR/index.html"
fi 