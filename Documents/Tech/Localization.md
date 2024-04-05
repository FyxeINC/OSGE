# Localization

## How to use

### Step 1

- Ensure you have a .csv Setup in the **/Localization** folder
    - Example: "Lang_en.cs"
- Entries should be (LanguageTag, StringTag, LocalizedString)
    - Example: "en, Localization.Panel, Panel" 

### Step 2 (Optional)

- Add static Tag to Tags.cs for less error prone code

### Step 3

- Call yourTag.GetString() to attempt to get a localizationTag
    - If your current language is not setup, it will return "LANG.NOT.FOUND"
    - If no localized string is found, it will return "STR.NOT.FOUND"