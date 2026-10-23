# Cesta Matfyzaka

Repozitář pro projekt Cesta Matfyzáka.
> [!important]
> Všechny příkazy (unless specified) jsou připraveny pro spouštění z kořene repozitáře.

## Inicializace Hry

Nejdříve pomocí následujícího příkazu vygenertujte prázdné schéma soutěže:
> [!caution]
> Tento příkaz nahradí stávající schéma, pokud v repozitáři nějaké již existuje!

```sh
dotnet run --project ./scripts -- -e > schema.json
```

Následně vygenerované schéma upravte, aby obsahovalo definici všech kategorií a her. Vlastnost `Order` značí pořadí v
rámci UI od nejmenšího nahoře, musí být unikátní.

## Inicializace týmu

Následující příkaz vypíše šablonu pro jeden tým.

```sh
dotnet run --project ./scripts -- -t
```

Tuto šablonu vložte do souboru s názvem tvaru `[identifier].team` (např. `team1.team`) do složky `/scores`. Pokud se tým
ve výstupu neobjevuje, je buď neplatný JSON uvnitř souboru daného týmu nebo daný soubor nemá příponu `.team`.

## Zapisování splnění hry

Otevřete si `.team` soubor daného týmu. Do seznamu ve spodní části souboru zapište jméno hry v následujícím formátu:

```json
{
  "Identifier": "team1",
  "PrettyName": "Team1",
  "CompletedGames": [
    "game1",
    "game3"
  ]
}
```

Poslední hra v pořadí za sebou nemá čárku, ostatní mají.