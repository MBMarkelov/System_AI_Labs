using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.Enums
{
    public enum EmailDomain
    {
        // Глобальные почтовые сервисы
        gmail_com,
        yahoo_com,
        outlook_com,
        hotmail_com,
        aol_com,
        protonmail_com,
        yandex_ru,
        mail_ru,
        rambler_ru,
        vk_ru,

        // Основные TLD
        com,
        net,
        org,
        info,
        biz,
        ru,
        ua,
        kz,
        by,
        de,
        fr,
        it,
        es,
        pl,
        cn,
        jp,

        // Образовательные
        edu,
        edu_ru,
        edu_ua,

        // Правительственные
        gov,
        gov_ru,
        mil,

        // Региональные
        moscow,
        spb_ru,
        uk_com,

        // Специализированные
        io,
        ai,
        dev,
        app,

        // Прочие
        custom,
        unknown
    }
}
